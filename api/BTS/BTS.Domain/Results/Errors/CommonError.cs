using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class CommonError
    {
        public static Error Unexpected(Exception exception) => new(ErrorType.Unexpected, nameof(exception), exception.ToString());
    }
}
