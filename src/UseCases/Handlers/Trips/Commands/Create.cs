using Ardalis.Result;
using AutoMapper;
using Domain.Entities.Model;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Commands;

public class CreateCommand : IRequest<Result<TripDto>>
{
    public CreateTripDto Value { get; set; } = new CreateTripDto();
}

public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<TripDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public CreateCommandHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<TripDto>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Value;

        var newTrip = _mapper.Map<Trip>(dto);

        try
        {
            await _context.Trips.AddAsync(newTrip, cancellationToken);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex) 
        {
            return Result.Error("save error");
        }

        var trip = await _context.Trips
            .Include(s => s.FromAddress)
            .Include(s => s.ToAddress)
            .SingleAsync(s => s.Id == newTrip.Id, cancellationToken);

        var tripDto = new TripDto(trip);

        return Result.Success(tripDto);
    }
}