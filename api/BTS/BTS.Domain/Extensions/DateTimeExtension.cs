namespace BTS.Domain.Extensions
{
    public class DateTimeExtension
    {
        public static DateTimeOffset GetCurrentDateTimeOffsetUtc() => DateTimeOffset.UtcNow;
        public static bool IsFutureDate(DateTime dateTime) => dateTime.Date > GetCurrentDateTimeOffsetUtc().Date;
    }
}
