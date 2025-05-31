using System.Globalization;

namespace FinancialsNice.Application.Helpers;

public static class DateConvertHelper
{
    public static DateOnly ParseDateOnly(string? dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString)) 
            throw new ArgumentException("Invalid date!", nameof(dateString));

        return DateOnly.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
    
    public static DateTime ToUtc(string dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString)) throw new ArgumentException("Invalid date!", nameof(dateString));
        var parsedDate = DateTime.ParseExact(
            dateString,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );
        var dateUtc = TimeZoneInfo.ConvertTimeToUtc(parsedDate);
        return dateUtc;
    }
}