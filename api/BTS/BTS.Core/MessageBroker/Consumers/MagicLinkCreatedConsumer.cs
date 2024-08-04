using BTS.Core.MessageBroker.Events.Authentication;
using BTS.Core.Services;
using MassTransit;

namespace BTS.Core.MessageBroker.Consumers
{
    public class MagicLinkCreatedConsumer : IConsumer<MagicLinkCreatedEvent>
    {
        private readonly IEmailService _emailService;
        public MagicLinkCreatedConsumer(IEmailService emailService) => _emailService = emailService;

        public async Task Consume(ConsumeContext<MagicLinkCreatedEvent> context) =>
            await _emailService.SendAsync(context.CancellationToken);
    }
}
