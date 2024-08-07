using BTS.Domain.Constants;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class UserError
    {
        public static Error Null => new(ErrorType.NULL, nameof(User), "User(s) result cannot be NULL.");
        public static Error NotFound => new(ErrorType.NotFound, nameof(User), string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, nameof(User)));
    }
}
