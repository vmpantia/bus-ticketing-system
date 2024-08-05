namespace BTS.Core.MessageBroker.Events.Authentication
{
    public class PasswordResetLinkCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
