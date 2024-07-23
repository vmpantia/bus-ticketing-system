using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Results.Errors
{
    public class DriverError
    {
        public static Error Null => new(ErrorType.NULL, nameof(Driver), "Driver(s) result cannot be NULL.");
        public static Error NotFound => new(ErrorType.NotFound, nameof(Driver), "Driver not found in the database.");
        public static Error DuplicateDriversLicenseNo => new(ErrorType.Duplicate, nameof(Driver), "Duplicate driver's license no. found in the database.");
    }
}
