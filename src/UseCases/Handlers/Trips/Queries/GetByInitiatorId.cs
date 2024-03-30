using Ardalis.Result;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Queries;

public class GetByInitiatorId : IRequest<Result<TripDtoList>>
{
    public string Id { get; set; } = default!;
}

internal class GetByInitiatorIdHandler : IRequestHandler<GetByInitiatorId, Result<TripDtoList>>
{
    private readonly IDbContext _dbContext;

    public GetByInitiatorIdHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TripDtoList>> Handle(GetByInitiatorId request, CancellationToken cancellationToken)
    {
        var result = new TripDtoList();

        var query = _dbContext.Trips.AsNoTracking();

        var trips = await query.Where(x => x.InitiatorId == request.Id)
            .Include(x => x.FromAddress)
            .Include(x => x.ToAddress)
            .ToListAsync(cancellationToken);

        if (!trips.Any())
        {
            return Result.NotFound();
        }

        var tripDtos = trips.Select(s => new TripDto(s));

        result.Value.AddRange(tripDtos);

        return result;
    }
}