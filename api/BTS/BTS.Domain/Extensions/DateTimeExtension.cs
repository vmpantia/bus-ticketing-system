namespace BTS.Domain.Extensions
{
    public class DateTimeExtension
    {
        public static DateTimeOffset GetCurrentDateTimeOffsetUtc() => DateTimeOffset.UtcNow;
        public static bool IsFutureDate(DateTime dateTime) => GetCurrentDateTimeOffsetUtc().Date > dateTime.Date;
        public static DateTime GetCurrentDateTimeUtc() => DateTime.UtcNow;
    }
}
