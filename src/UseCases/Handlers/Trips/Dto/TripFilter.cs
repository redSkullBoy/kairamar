namespace UseCases.Handlers.Trips.Dto;

public class TripFilter
{
    public int FromAddressId { get; set; }
    public string FromAddress { get; set; } = default!;

    public int ToAddressId { get; set; }
    public string ToAddress { get; set; } = default!;

    public DateTime StartDateLocal { get; set; }

    public int? RequestedSeats { get; set; } = 1;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
