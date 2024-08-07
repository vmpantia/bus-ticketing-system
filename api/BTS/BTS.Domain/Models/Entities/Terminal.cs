using BTS.Domain.Contractors.Models;
using BTS.Domain.Contractors.Models.Entities;
using BTS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Entities
{
    public class Terminal : IEntity<Guid, CommonStatus>
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Provice { get; set; }
        public required string City { get; set; }
        public required string PlusCode { get; set; }
        public string? Description { get; set; }
        public required CommonStatus Status { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public required virtual ICollection<Route> OriginRoutes { get; set; }
        public required virtual ICollection<Route> DestinationRoutes { get; set; }
    }
}
