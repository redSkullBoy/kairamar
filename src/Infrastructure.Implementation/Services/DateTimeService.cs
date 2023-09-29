using Infrastructure.Interfaces.Services;

namespace Infrastructure.Implementation.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
