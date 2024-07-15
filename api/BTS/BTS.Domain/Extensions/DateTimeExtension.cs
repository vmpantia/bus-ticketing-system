namespace BTS.Domain.Extensions
{
    public class DateTimeExtension
    {
        public static DateTime GetCurrentDateByUtc() => DateTime.UtcNow;
    }
}
