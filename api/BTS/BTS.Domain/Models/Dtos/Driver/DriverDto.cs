using BTS.Domain.Models.Lites;

namespace BTS.Domain.Models.Dtos.Driver
{
    public class DriverDto
    {
        public required string LicenseNo { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public required string ContactNo { get; set; }
        public required DateTime Birthdate { get; set; }

        // Related Data
        public required BusLite Bus { get; set; }

        public required DateTimeOffset LastUpdateAt { get; set; }
        public required string LastUpdateBy { get; set; }
    }
}
