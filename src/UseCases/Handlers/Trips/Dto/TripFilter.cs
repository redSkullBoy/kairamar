namespace UseCases.Handlers.Trips.Dto;

public class TripFilter
{
    public int FromAddressId { get; set; }

    public int ToAddressId { get; set; }

    public DateTime StartDateLocal { get; set; }

    public int RequestedSeats { get; set; } = 1;
}
