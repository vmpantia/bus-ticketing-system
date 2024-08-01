using BTS.Domain.Models.Lites;

namespace BTS.Domain.Models.Dtos.Bus
{
    public class BusDto
    {
        public required Guid Id { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }

        // Related Data
        public required DriverLite? Driver { get; set; }

        public required DateTimeOffset LastUpdateAt { get; set; }
        public required string LastUpdateBy { get; set; }
    }
}
