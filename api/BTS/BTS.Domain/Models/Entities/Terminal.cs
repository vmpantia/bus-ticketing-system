using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class Terminal : BaseEntity<Guid, CommonStatus>
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Provice { get; set; }
        public required string City { get; set; }
        public required string PlusCode { get; set; }
        public string? Description { get; set; }

        public required virtual ICollection<Route> OriginRoutes { get; set; }
        public required virtual ICollection<Route> DestinationRoutes { get; set; }
    }
}
