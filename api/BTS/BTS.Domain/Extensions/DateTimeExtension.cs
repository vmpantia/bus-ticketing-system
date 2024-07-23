namespace BTS.Domain.Extensions
{
    public class DateTimeExtension
    {
        public static DateTimeOffset GetCurrentDateTimeOffsetUtc() => DateTimeOffset.UtcNow;
    }
}
