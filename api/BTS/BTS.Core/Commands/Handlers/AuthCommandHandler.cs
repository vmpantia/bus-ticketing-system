using BTS.Core.Commands.Models.Authentication;
using BTS.Core.MessageBroker.Events.Authentication;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Domain.Results;
using MassTransit;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class AuthCommandHandler :
        IRequestHandler<LoginByCredentialsCommand, Result>,
        IRequestHandler<LoginByEmailCommand, Result>,
        IRequestHandler<LoginByTokenCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuthCommandHandler(IUserRepository userRepository,
                                  IAccessTokenRepository accessTokenRepository,
                                  IAuthenticationService authenticationService,
                                  IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _accessTokenRepository = accessTokenRepository;
            _authenticationService = authenticationService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result> Handle(LoginByCredentialsCommand request, CancellationToken cancellationToken) =>
            await Task.Run(() =>
            {
                var token = _authenticationService.AuthenticateByCredentials(request.UsernameOrEmail, request.Password, out User user);
                return Result.Success(new
                {
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = token
                });
            });

        public async Task<Result> Handle(LoginByEmailCommand request, CancellationToken cancellationToken)
        {
            // Generate token used for magic link
            var token = _authenticationService.AuthenticateByEmail(request.Email, out User user);

            // Create a record for the generated token
            var result = await _accessTokenRepository.CreateAsync(new AccessToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                Type = AccessTokenType.MagicLink,
                CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc(),
                CreatedBy = request.Email
            }, cancellationToken);

            // Send a event message to message broker
            await _publishEndpoint.Publish(new MagicLinkCreatedEvent
            {
                Id = result.Id,
                UserId = result.UserId,
            }, cancellationToken);

            return Result.Success("Magic link created, Kindly check on your email.");
        }

        public async Task<Result> Handle(LoginByTokenCommand request, CancellationToken cancellationToken) =>
            await Task.Run(() =>
            {
                var token = _authenticationService.AuthenticateByToken(request.Token, out User user);
                return Result.Success(new
                {
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = token
                });
            });
    }
}
