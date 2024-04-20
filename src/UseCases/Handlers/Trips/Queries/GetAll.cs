using Ardalis.Result;
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
                                .Include(s => s.FromAddress)
                                .Include(s => s.ToAddress)
                                .Where(x => x.StartDateLocal >= request.Value.StartDateLocal
                                        && x.StartDateLocal <= startPeriod);

        var from = await _dbContext.Addresses.FirstOrDefaultAsync(s => s.Id == request.Value.FromAddressId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(from?.City))
        {
            query = query.Where(s => s.FromAddress.Settlement == from.City || s.FromAddress.City == from.City);
        }
        if (!string.IsNullOrWhiteSpace(from?.Settlement))
        {
            query = query.Where(s => s.FromAddress.Settlement == from.Settlement || s.FromAddress.City == from.Settlement);
        }

        var to = await _dbContext.Addresses.FirstOrDefaultAsync(s => s.Id == request.Value.ToAddressId, cancellationToken);

        if (!string.IsNullOrWhiteSpace(from?.City))
        {
            query = query.Where(s => s.FromAddress.Settlement == from.City || s.FromAddress.City == from.City);
        }
        if (!string.IsNullOrWhiteSpace(from?.Settlement))
        {
            query = query.Where(s => s.FromAddress.Settlement == from.Settlement || s.FromAddress.City == from.Settlement);
        }

        var tripDb = await _dbContext.PaginatedListAsync(query, request.Value.PageNumber, request.Value.PageSize, cancellationToken);

        var tripDtos = tripDb.value.Select(s => new TripDto(s)).ToList();

        return PaginatedResult<TripDto>.Success(tripDtos, tripDb.count, request.Value.PageNumber, request.Value.PageSize);
    }
}