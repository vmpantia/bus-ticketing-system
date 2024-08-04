using BTS.Core.MessageBroker.Events.Authentication;
using BTS.Core.Services;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Exceptions;
using BTS.Domain.Models.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BTS.Core.MessageBroker.Consumers
{
    public class MagicLinkCreatedConsumer : IConsumer<MagicLinkCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly ILogger<MagicLinkCreatedConsumer> _logger;
        public MagicLinkCreatedConsumer(IEmailService emailService,
                                        IUserRepository userRepository,
                                        IAccessTokenRepository accessTokenRepository,
                                         ILogger<MagicLinkCreatedConsumer> logger)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _accessTokenRepository = accessTokenRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MagicLinkCreatedEvent> context)
        {
            try
            {
                if (!_accessTokenRepository.IsExist(data => data.Id == context.Message.Id &&
                                                            data.UserId == context.Message.UserId, out AccessToken accessToken))
                    throw new NotFoundException(string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, "Token"));

                if (!_userRepository.IsExist(data => data.Id == context.Message.UserId, out User user))
                    throw new NotFoundException(string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, nameof(User)));

                await _emailService.SendMagicLinkEmail(accessToken, user, context.CancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send magic link from {nameof(MagicLinkCreatedConsumer)} due to error {ex.Message}");
                return;
            }
        }
    }
}
