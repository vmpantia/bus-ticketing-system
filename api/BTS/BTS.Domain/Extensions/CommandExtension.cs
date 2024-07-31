namespace BTS.Domain.Extensions
{
    public class CommandExtension
    {
        public static bool IsNotCommand<TRequest>() => !typeof(TRequest).Name.EndsWith("Command");
    }
}
