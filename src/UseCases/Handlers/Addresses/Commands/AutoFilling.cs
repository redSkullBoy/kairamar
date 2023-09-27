using Ardalis.Result;
using AutoMapper;
using Domain.Entities.AppDb;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Dto;
using Infrastructure.Interfaces.Services;
using MediatR;

namespace UseCases.Handlers.Addresses.Commands;

public class AutoFillingCommand : IRequest<Result<List<AddressDataDto>>>
{
    public string? Text { get; set; }
}

public class AutoFillingCommandHandler : IRequestHandler<AutoFillingCommand, Result<List<AddressDataDto>>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDataRefineService _dataRefineService;

    public AutoFillingCommandHandler(IDbContext context, IMapper mapper, IDataRefineService dataRefineService)
    {
        _context = context;
        _mapper = mapper;
        _dataRefineService = dataRefineService;
    }

    public async Task<Result<List<AddressDataDto>>> Handle(AutoFillingCommand request, CancellationToken cancellationToken)
    {
        var result = await _dataRefineService.AddressAutoFillingAsync(request.Text!);

        if(result.IsSuccess)
        {
            //заполнение новых адресов с Dadata
            var fiasIds = result.Value.Select(s => s.FiasId).ToList();

            var addressesDb = _context.Addresses.Where(s => fiasIds.Contains(s.FiasId)).ToList();

            if(addressesDb.Any())
            {
                foreach(var address in result.Value)
                {
                    bool isNewAddress = !addressesDb.Any(s => s.FiasId == address.FiasId);

                    if (isNewAddress)
                    {
                        var addressDb = _mapper.Map<Address>(result.Value);

                        await _context.Addresses.AddAsync(addressDb);
                    }
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                try
                {
                    var newAddresses = _mapper.Map<List<Address>>(result.Value);

                    await _context.Addresses.AddRangeAsync(newAddresses);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }

        return result;
    }
}
