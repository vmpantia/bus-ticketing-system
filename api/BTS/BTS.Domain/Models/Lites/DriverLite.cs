namespace BTS.Domain.Models.Lites
{
    public class DriverLite
    {
        public required Guid Id { get; set; }
        public required string LicenseNo { get; set; }
        public required string Name { get; set; }
    }
}
