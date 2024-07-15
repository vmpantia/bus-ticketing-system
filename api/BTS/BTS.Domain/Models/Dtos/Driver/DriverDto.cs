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

        // Bus Information
        public required int BusId { get; set; }
        public required string BusNo { get; set; }
        public required string BusPlateNo { get; set; }

        public required DateTime LastUpdateAt { get; set; }
        public required string LastUpdateBy { get; set; }
    }
}
