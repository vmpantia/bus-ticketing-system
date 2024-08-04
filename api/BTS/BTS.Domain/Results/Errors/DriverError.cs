using BTS.Domain.Constants;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class DriverError
    {
        public static Error Null => new(ErrorType.NULL, nameof(Driver), "Driver(s) result cannot be NULL.");
        public static Error NotFound => new(ErrorType.NotFound, nameof(Driver), string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, nameof(Driver)));
    }
}
