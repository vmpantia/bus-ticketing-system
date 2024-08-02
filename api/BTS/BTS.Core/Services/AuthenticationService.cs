using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using BTS.Domain.Contractors.Services;

namespace BTS.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public AuthenticationService(IUserRepository userRepository,
                                     IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public Result Authenticate(string userNameOrEmail, string password)
        {
            // Check if the user exist based on the userName or Email
            if (!_userRepository.IsExist(data => data.Username.Equals(userNameOrEmail) ||
                                                 data.Email.Equals(userNameOrEmail),
                                        out User user))
                return Result.Failure(UserError.NotFound);

            // Check if the user password matched on the input password
            if (!user.Password.Equals(password))
                return Result.Failure(UserError.InvalidCredentials);

            // Generate access token for user
            var token = _jwtProvider.Generate(user);

            return Result.Success(new
            {
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                Token = token,
            });
        }
    }
}
