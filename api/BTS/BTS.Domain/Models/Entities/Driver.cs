using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class Driver : BaseEntity<Guid, CommonStatus>
    {
        public required string LicenseNo { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public required string ContactNo { get; set; }
        public required DateTime Birthdate { get; set; }

        public required virtual Bus Bus { get; set; }
    }
}
