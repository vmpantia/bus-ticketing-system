namespace BTS.Core.MessageBroker.Events.Authentication
{
    public class ResetPasswordLinkCreatedEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
