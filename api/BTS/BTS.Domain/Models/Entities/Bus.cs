using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class Bus : BaseEntity<Guid, CommonStatus>
    {
        public Guid? DriverId { get; set; }
        public Guid? RouteId { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }

        public virtual Driver? Driver { get; set; }
        public virtual Route? Route { get; set; }
    }
}
