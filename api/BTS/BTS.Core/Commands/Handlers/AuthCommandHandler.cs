using BTS.Core.Commands.Models.Auth;
using BTS.Core.Services;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Email;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class AuthCommandHandler :
        IRequestHandler<LoginCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
        public AuthCommandHandler(IUserRepository userRepository,
                                  IAuthenticationService authenticationService,
                                  IEmailService emailService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var token = _authenticationService.Authenticate(request.UsernameOrEmail, request.Password, out User user);

            // Send test email
            await _emailService.SendEmail(cancellationToken);

            return Result.Success(new
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Token = token
            });
        }
    }
}
