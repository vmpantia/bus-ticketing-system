using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class User : BaseEntity<Guid, CommonStatus>
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }

        // Access Control
        public required bool IsEmailConfirmed { get; set; }
        public required bool IsAdmin { get; set; }
    }
}
