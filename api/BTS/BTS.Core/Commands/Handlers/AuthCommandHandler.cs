using BTS.Core.Commands.Models.Auth;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Contractors.Services;
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
        public AuthCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken) =>
            await Task.Run(() =>
            {
                var token = _authenticationService.Authenticate(request.UsernameOrEmail, request.Password, out User user);
                return Result.Success(new
                {
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = token
                });
            });
    }
}
