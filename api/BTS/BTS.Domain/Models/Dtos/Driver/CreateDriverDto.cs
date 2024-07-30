namespace BTS.Domain.Models.Dtos.Driver
{
    public class CreateDriverDto
    {
        public required string LicenseNo { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public required string ContactNo { get; set; }
        public required DateTime Birthdate { get; set; }
    }
}
