namespace BTS.Domain.Models.Lites
{
    public class RouteLite
    {
        public required Guid Id { get; set; }
        public required Guid OriginTerminalId { get; set; }
        public required Guid DestinationTerminalId { get; set; }
    }
}
