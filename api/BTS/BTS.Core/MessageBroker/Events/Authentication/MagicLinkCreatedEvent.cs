namespace BTS.Core.MessageBroker.Events.Authentication
{
    public class MagicLinkCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
