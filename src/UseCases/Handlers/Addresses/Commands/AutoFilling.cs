using ApplicationServices.Interfaces;
using Ardalis.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Model;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Addresses.Dto;

namespace UseCases.Handlers.Addresses.Commands;

public class AutoFillingCommand : IRequest<Result<List<AddressDto>>>
{
    public string? Text { get; set; }
}

public class AutoFillingCommandHandler : IRequestHandler<AutoFillingCommand, Result<List<AddressDto>>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDataRefineService _dataRefineService;
    private readonly IAddressService _addressService;

    public AutoFillingCommandHandler(IDbContext context, IMapper mapper, IDataRefineService dataRefineService, IAddressService addressService)
    {
        _context = context;
        _mapper = mapper;
        _dataRefineService = dataRefineService;
        _addressService = addressService;
    }

    public async Task<Result<List<AddressDto>>> Handle(AutoFillingCommand request, CancellationToken cancellationToken)
    {
        var addressesId = new List<int>();
        var newAddresses = new List<Address>();

        var result = await _dataRefineService.AddressAutoFillingAsync(request.Text!, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.NotFound();
        }

        foreach(var addressDadata in result.Value)
        {
            var anyAddressDb = await _context.Addresses.AnyAsync(s => s.Note == addressDadata.Note);

            if (anyAddressDb)
            {
                var addressDbId = await _context.Addresses.Where(s => s.Note == addressDadata.Note).Select(s => s.Id).FirstAsync();
                addressesId.Add(addressDbId);
            }
            else
            {
                var address = _mapper.Map<Address>(addressDadata);
                newAddresses.Add(address);
            }
        }

        await _context.Addresses.AddRangeAsync(newAddresses, cancellationToken);
        await _context.SaveChangesAsync();

        addressesId.AddRange(newAddresses.Select(s => s.Id));

        var addressDto = await _context.Addresses
                                    .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
                                    .Where(s => addressesId.Contains(s.Id))
                                    .ToListAsync();

        return Result.Success(addressDto);
    }
}