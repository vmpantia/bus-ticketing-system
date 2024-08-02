using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Contractors.Services;
using BTS.Domain.Models.Entities;

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

        public string Authenticate(string userNameOrEmail, string password, out User user)
        {
            // Get user based on the credentials provided
            user = _userRepository.GetOne(data => (data.Username.Equals(userNameOrEmail) ||
                                                   data.Email.Equals(userNameOrEmail)) &&
                                                  data.Password.Equals(password));

            // Generate access token for user
            var accessToken = _jwtProvider.Generate(user);

            return accessToken;
        }
    }
}
