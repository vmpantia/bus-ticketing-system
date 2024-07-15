using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class Bus : BaseEntity<Guid, CommonStatus>
    {
        public required Guid DriverId { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }

        public required virtual Driver Driver { get; set; }
        public required virtual ICollection<Route> Routes { get; set; }
    }
}
