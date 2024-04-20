using Ardalis.Result;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Handlers.Trips.Queries;

public class CoincidSettlementRequest : IRequest<Result>
{
    public int FromAddressId { get; set; }

    public int ToAddressId { get; set; }
}

internal class CoincidSettlementRequestHandler : IRequestHandler<CoincidSettlementRequest, Result>
{
    private readonly IDbContext _dbContext;

    public CoincidSettlementRequestHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CoincidSettlementRequest request, CancellationToken cancellationToken)
    {
        var from = await _dbContext.Addresses.FirstOrDefaultAsync(s => s.Id == request.FromAddressId, cancellationToken);

        var to = await _dbContext.Addresses.FirstOrDefaultAsync(s => s.Id == request.ToAddressId, cancellationToken);

        if (from?.Settlement != null &&
            (from.Settlement == to?.Settlement || from.Settlement == to?.City))
        {
            return Result.Error();
        }

        if (from?.City != null &&
           (from.City == to?.Settlement || from.City == to?.City))
        {
            return Result.Error();
        }

        return Result.Success();
    }
}