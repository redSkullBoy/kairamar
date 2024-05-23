namespace Infrastructure.Interfaces.Services;

public interface IDateTime
{
    public DateTime MoscowNow();

    public DateTime TimeZoneNow(string timeZoneId);

    public DateTime ConvertToTimeZone(DateTime dateTime, string timeZoneId);

    public string ToRussianString(DateTime dateTime);

    public string ToString(DateTime dateTime, string format);
}
