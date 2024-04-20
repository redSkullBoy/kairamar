

namespace Domain.Entities.Model;

public class TripPassenger
{
    public int Id { get; set; }

    public int TripId { get; set; }

    public string PassengerId { get; set; } = string.Empty;
    public AppUser? Passenger { get; set; }

    public int AmountSeats { get; set; }
}
