using DataAccess.Sqlite;

namespace UseCases.Handlers.Trips.Dto;

public class CreateTripDto
{
    public int FromAddressId { get; set; }

    public int ToAddressId { get; set; }

    public string Locale { get; set; } = string.Empty;

    public DateTime? StartDateLocal { get; set; } = DateTime.Now;
    public DateTime? EndDateLocal { get; set; } = DateTime.Now.AddHours(1);

    public int RequestedSeats { get; set; }

    public int? RadiusInMeters { get; set; } = 5000;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string InitiatorId { get; set; } = string.Empty;
}
