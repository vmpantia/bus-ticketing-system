using BTS.Core.Commands.Models.Authentication;
using BTS.Core.MessageBroker.Events.Authentication;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Results;
using MassTransit;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class AuthCommandHandler :
        IRequestHandler<LoginCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuthCommandHandler(IUserRepository userRepository,
                                  IAuthenticationService authenticationService,
                                  IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = _authenticationService.Authenticate(request.UsernameOrEmail, request.Password, out User user);

            // Send magic link created event to a consumer
            await _publishEndpoint.Publish(new MagicLinkCreatedEvent
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc()
            }, cancellationToken);

            return Result.Success(new
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Token = token
            });
        }
    }
}
