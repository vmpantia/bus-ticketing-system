using BTS.Domain.Contractors.Models.Entities;
using BTS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Entities
{
    public class Driver : IEntity<Guid, CommonStatus>
    {
        [Key]
        public required Guid Id { get; set; }
        public required string LicenseNo { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public required string ContactNo { get; set; }
        public required DateTime Birthdate { get; set; }
        public required CommonStatus Status { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public required virtual Bus Bus { get; set; }
    }
}
