using Infrastructure.Interfaces.Services;

namespace Infrastructure.Implementation.Services;

public class DateTimeService : IDateTime
{
    public DateTime MoscowNow()
    {
        var now = DateTime.UtcNow;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(now, timeZone);
    }

    public DateTime TimeZoneNow(string? timeZoneId)
    {
        var now = DateTime.UtcNow;

        if (timeZoneId == null)
            return now;

        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(now, timeZone);
    }

    public DateTime ConvertToTimeZone(DateTime dateTime, string timeZoneId)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
    }

    public string ToRussianString(DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy HH:mm");
    }

    public string ToString(DateTime dateTime, string format)
    {
        return dateTime.ToString(format);
    }
}
