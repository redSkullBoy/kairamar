namespace UseCases.Handlers.Trips.Dto;

public class TripDto
{
    public int Id { get; set; }

    public int FromAddressId { get; set; }

    public int ToAddressId { get; set; }

    public string Locale { get; set; } = string.Empty;

    public DateTime StartDateLocal { get; set; }
    public DateTime EndDateLocal { get; set; }

    public int RequestedSeats { get; set; }

    public int RadiusInMeters { get; set; } = 5000;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string InitiatorId { get; set; } = string.Empty;
}
