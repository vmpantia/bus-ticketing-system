using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Core.Results.Errors
{
    public class BusError
    {
        public static Error Null => new(ErrorType.NULL, nameof(Bus), "Bus(s) result cannot be NULL.");
        public static Error NotFound => new(ErrorType.NotFound, nameof(Bus), "Bus not found in the database.");
    }
}
