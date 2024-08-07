using BTS.Domain.Contractors.Models;
using BTS.Domain.Contractors.Models.Entities;
using BTS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Entities
{
    public class Bus : IEntity<Guid, CommonStatus>
    {
        [Key]
        public required Guid Id { get; set; }
        public Guid? DriverId { get; set; }
        public Guid? RouteId { get; set; }
        public required string BusNo { get; set; }
        public required string PlateNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }
        public required CommonStatus Status { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public virtual Driver? Driver { get; set; }
        public virtual Route? Route { get; set; }
    }
}
