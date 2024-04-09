﻿using Ardalis.Result;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Queries;

public class GetAllRequest : IRequest<PaginatedResult<TripDto>>
{
    public TripFilter Value { get; set; } = default!;
}

internal class GetAllRequestHandler : IRequestHandler<GetAllRequest, PaginatedResult<TripDto>>
{
    private readonly IDbContext _dbContext;

    public GetAllRequestHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginatedResult<TripDto>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var startPeriod = request.Value.StartDateLocal.AddDays(1);

        var query = _dbContext.Trips.AsNoTracking()
                                .Include(x => x.TripPassenger)
                                .Include(x => x.Initiator)
                                .Where(x => x.FromAddressId == request.Value.FromAddressId 
                                        && x.ToAddressId == request.Value.ToAddressId
                                        && x.StartDateLocal >= request.Value.StartDateLocal
                                        && x.StartDateLocal <= startPeriod);

        var tripDb = await _dbContext.PaginatedListAsync(query, request.Value.PageNumber, request.Value.PageSize, cancellationToken);

        var tripDtos = tripDb.value.Select(s => new TripDto(s)).ToList();

        return PaginatedResult<TripDto>.Success(tripDtos, tripDb.count, request.Value.PageNumber, request.Value.PageSize);
    }
}