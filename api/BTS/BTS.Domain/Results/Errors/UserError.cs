using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class UserError
    {
        public static Error NotFound => new(ErrorType.NotFound, nameof(User), "User not found in the database.");
        public static Error InvalidCredentials => new (ErrorType.InvalidCredentials, nameof(User), "Invalid user credentials.");
    }
}
