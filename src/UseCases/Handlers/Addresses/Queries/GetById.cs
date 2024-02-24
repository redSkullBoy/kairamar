using Ardalis.Result;
using AutoMapper;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Addresses.Dto;

namespace UseCases.Handlers.Addresses.Queries;

public class GetByIdRequest : IRequest<Result<AddressDto>>
{
    public int Id { get; set; } = default!;
}

internal class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<AddressDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetByIdRequestHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<AddressDto>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var address = await _dbContext.Addresses.AsNoTracking().SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (address == null)
            return Result.NotFound();

        var addressesDto = _mapper.Map<AddressDto>(address);

        return addressesDto;
    }
}