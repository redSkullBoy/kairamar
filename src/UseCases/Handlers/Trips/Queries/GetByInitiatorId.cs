using Ardalis.Result;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Queries;

public class GetByInitiatorId : IRequest<PaginatedResult<TripDto>>
{
    public string Id { get; set; } = default!;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

internal class GetByInitiatorIdHandler : IRequestHandler<GetByInitiatorId, PaginatedResult<TripDto>>
{
    private readonly IDbContext _dbContext;

    public GetByInitiatorIdHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginatedResult<TripDto>> Handle(GetByInitiatorId request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Trips.AsNoTracking();

        var trips = await query.Where(x => x.InitiatorId == request.Id)
            .Include(x => x.FromAddress)
            .Include(x => x.ToAddress)
            .ToListAsync(cancellationToken);

        if (!trips.Any())
        {
            return PaginatedResult<TripDto>.NotFound();
        }

        var tripDb = await _dbContext.PaginatedListAsync(query, request.PageNumber, request.PageSize, cancellationToken);

        var tripDtos = tripDb.value.Select(s => new TripDto(s)).ToList();

        return PaginatedResult<TripDto>.Success(tripDtos, tripDb.count, request.PageNumber, request.PageSize);
    }
}