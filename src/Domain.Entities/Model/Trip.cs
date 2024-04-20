using DataAccess.Sqlite;
using Domain.Entities.Common;

namespace Domain.Entities.Model;

public class Trip : BaseAuditableEntity
{
    public int FromAddressId { get; set; }
    public Address? FromAddress { get; set; } = null;

    public int ToAddressId { get; set; }
    public Address? ToAddress { get; set; } = null;

    public string Locale { get; set; } = string.Empty;

    public DateTime StartDateLocal { get; set; }
    public DateTime EndDateLocal { get; set;}

    public int RequestedSeats { get; set; }

    public int RadiusInMeters { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string InitiatorId { get; set; } = string.Empty;
    public AppUser? Initiator { get; set; }

    public TripPassenger? TripPassenger { get; set; }
}
