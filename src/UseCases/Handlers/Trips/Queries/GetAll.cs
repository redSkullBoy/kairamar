using Ardalis.Result;
using AutoMapper;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Queries;

public class GetAllRequest : IRequest<Result<TripDtoList>>
{
    public TripFilter Value { get; set; } = default!;
}

internal class GetAllRequestHandler : IRequestHandler<GetAllRequest, Result<TripDtoList>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllRequestHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<TripDtoList>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var result = new TripDtoList();

        var startPeriod = request.Value.StartDateLocal.AddDays(1);

        var query = _dbContext.Trips.AsNoTracking()
                                .Include(x => x.TripPassenger);


        var tripDb = await query.Where(x => x.FromAddressId == request.Value.FromAddressId 
                                                && x.ToAddressId == request.Value.ToAddressId
                                                && x.StartDateLocal >= request.Value.StartDateLocal
                                                && x.StartDateLocal <= startPeriod)
                                                .ToListAsync(cancellationToken);

        var tripDtos = _mapper.Map<List<TripDto>>(tripDb);

        result.Value.AddRange(tripDtos);

        return result;
    }
}