using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Dtos.Driver
{
    public class CreateDriverDto
    {
        [Required]
        public required string LicenseNo { get; set; }
        [Required]
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required, AllowedValues("Male", "Female", "Undecided")]
        public required string Gender { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string ContactNo { get; set; }
        [Required]
        public required DateTime Birthdate { get; set; }
    }
}
