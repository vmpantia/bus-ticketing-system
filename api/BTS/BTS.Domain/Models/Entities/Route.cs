using BTS.Domain.Contractors.Models;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Models.Entities
{
    public class Route : BaseEntity<Guid, CommonStatus>
    {
        public required Guid BusId { get; set; }
        public required Guid OriginTerminalId { get; set; }
        public required Guid DestinationTerminalId { get; set; }

        public required virtual Bus Bus { get; set; }
        public required virtual Terminal OriginTerminal { get; set; }
        public required virtual Terminal DestinationTerminal { get; set; }
    }
}
