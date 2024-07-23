namespace BTS.Domain.Models.Lites
{
    public class BusLite
    {
        public required Guid Id { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
    }
}
