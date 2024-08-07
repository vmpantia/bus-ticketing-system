using BTS.Core.Commands.Models.Authentication;
using BTS.Core.MessageBroker.Events.Authentication;
using BTS.Domain.Constants;
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
        IRequestHandler<LoginByTokenCommand, Result>,
        IRequestHandler<LoginByEmailCommand, Result>,
        IRequestHandler<ResetPasswordByEmailCommand, Result>,
        IRequestHandler<UpdatePasswordCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuthCommandHandler(IUserRepository userRepository,
                                  IAccessTokenRepository accessTokenRepository,
                                  IAuthenticationService authenticationService,
                                  IJwtProvider jwtProvider,
                                  IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _accessTokenRepository = accessTokenRepository;
            _authenticationService = authenticationService;
            _jwtProvider = jwtProvider;
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

        public async Task<Result> Handle(LoginByTokenCommand request, CancellationToken cancellationToken)
        {
            // Get email from the token provided
            var email = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_EMAIL, request.Token);

            // Generate new token from the token provided
            var newToken = _authenticationService.AuthenticateByToken(request.Token, out User user);

            // Update magic link token to used
            await _accessTokenRepository.UpdateToUsedAsync(user.Id, 
                                                           request.Token, 
                                                           AccessTokenType.MagicLink,
                                                           user.Email, 
                                                           cancellationToken);

            return Result.Success(new
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Token = newToken
            });
        }

        public async Task<Result> Handle(LoginByEmailCommand request, CancellationToken cancellationToken)
        {
            // Generate token used for magic link
            var token = _authenticationService.AuthenticateByEmail(request.Email, AccessTokenType.MagicLink, out User user);

            // Check if the generated token is NULL or empty
            if(!string.IsNullOrEmpty(token))
            {
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
            }

            return Result.Success();
        }

        public async Task<Result> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            // Generate token used for password reset link
            var token = _authenticationService.AuthenticateByEmail(request.Email, AccessTokenType.ResetPasswordLink, out User user);

            // Check if the generated token is NULL or empty
            if (!string.IsNullOrEmpty(token))
            {
                // Create a record for the generated token
                var result = await _accessTokenRepository.CreateAsync(new AccessToken
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Token = token,
                    Type = AccessTokenType.ResetPasswordLink,
                    CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc(),
                    CreatedBy = request.Email
                }, cancellationToken);

                // Send a event message to message broker
                await _publishEndpoint.Publish(new ResetPasswordLinkCreatedEvent
                {
                    Id = result.Id,
                    UserId = result.UserId,
                }, cancellationToken);
            }

            return Result.Success();
        }

        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            // Get email from the reset password token
            var email = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_EMAIL, request.Token);

            // Check if the email exist on the database
            var user = _userRepository.GetOne(data => data.Email.Equals(email) &&
                                                      data.Status == CommonStatus.Active);

            // Update reset password token to used
            await _accessTokenRepository.UpdateToUsedAsync(user.Id,
                                                           request.Token,
                                                           AccessTokenType.ResetPasswordLink,
                                                           user.Email,
                                                           cancellationToken);

            // Update user necessary information
            user.Password = request.NewPassword;
            user.UpdatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            user.UpdatedBy = email;
            await _userRepository.UpdateAsync(user, cancellationToken);

            return Result.Success();
        }
    }
}
