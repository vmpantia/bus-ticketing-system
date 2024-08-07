using BTS.Domain.Contractors.Models.Entities;
using BTS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Entities
{
    public class User : IEntity<Guid, CommonStatus>
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required CommonStatus Status { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Access Control
        public required bool IsEmailConfirmed { get; set; }
        public required bool IsAdmin { get; set; }
    }
}
