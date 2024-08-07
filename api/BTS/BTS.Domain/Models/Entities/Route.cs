using BTS.Domain.Contractors.Models.Entities;
using BTS.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTS.Domain.Models.Entities
{
    public class Route : IEntity<Guid, CommonStatus>
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid OriginTerminalId { get; set; }
        public required Guid DestinationTerminalId { get; set; }
        public required CommonStatus Status { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public required virtual Bus Bus { get; set; }
        public required virtual Terminal OriginTerminal { get; set; }
        public required virtual Terminal DestinationTerminal { get; set; }
    }
}
