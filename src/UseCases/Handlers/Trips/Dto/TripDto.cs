using Domain.Entities.Model;

namespace UseCases.Handlers.Trips.Dto;
public class TripDto
{
    public TripDto() { }

    public TripDto(Trip trip)
    {
        Id = trip.Id;
        FromAddressId = trip.FromAddressId;
        FromAddressName = trip.FromAddress?.Value ?? string.Empty;
        ToAddressId = trip.ToAddressId;
        ToAddressName = trip.ToAddress?.Value ?? string.Empty;
        Locale = trip.Locale;
        StartDateLocal = trip.StartDateLocal;
        EndDateLocal = trip.EndDateLocal;
        RequestedSeats = trip.RequestedSeats;
        RadiusInMeters = trip.RadiusInMeters;
        Description = trip.Description;
        Price = trip.Price;
        InitiatorId = trip.InitiatorId;
        InitiatorName = trip.Initiator?.UserName;
        InitiatorPhone = trip.Initiator?.PhoneNumber;
    }

    public int Id { get; set; }

    public int FromAddressId { get; set; }
    public string FromAddressName { get; set; } = string.Empty;

    public int ToAddressId { get; set; }
    public string ToAddressName { get; set; } = string.Empty;

    public string Locale { get; set; } = string.Empty;

    public DateTime StartDateLocal { get; set; }
    public DateTime EndDateLocal { get; set; }

    public int RequestedSeats { get; set; }

    public int RadiusInMeters { get; set; } = 5000;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string InitiatorId { get; set; } = string.Empty;
    public string? InitiatorName { get; set; } = null;

    public string? InitiatorPhone { get; set; }
}
