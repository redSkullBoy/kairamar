using DataAccess.Sqlite;

namespace Domain.Entities.Model;

public class TripCompanion
{
    public int Id { get; set; }

    public int TripId { get; set; }

    public string CompanionId { get; set; } = string.Empty;
    public AppUser? Companion { get; set; }

    public int AmountSeats { get; set; }
}
