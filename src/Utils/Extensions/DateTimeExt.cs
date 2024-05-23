using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions;
public static class DateTimeExt
{
    public static DateTime MoscowNow()
    {
        var now = DateTime.UtcNow;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(now, timeZone);
    }

    public static DateTime TimeZoneNow(string timeZoneId)
    {
        var now = DateTime.UtcNow;
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(now, timeZone);
    }

    public static DateTime ConvertToTimeZone(DateTime dateTime, string timeZoneId)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
    }

    public static string ToRussianString(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy HH:mm");
    }

    public static string ToString(DateTime dateTime, string format)
    {
        return dateTime.ToString(format);
    }
}
