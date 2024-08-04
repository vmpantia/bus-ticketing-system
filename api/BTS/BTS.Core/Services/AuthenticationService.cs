using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using System.Security.Claims;

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
                                                  data.Password.Equals(password) &&
                                                  data.Status == CommonStatus.Active);

            // Create user claims use for generating access token
            var claims = new List<Claim>()
                  .AddUserId(user.Id)
                  .AddUserName(user.FirstName, user.LastName)
                  .AddUserEmail(user.Email)
                  .AddUserRole(user.IsAdmin);

            // Generate access token for user
            var accessToken = _jwtProvider.GenerateToken(user, claims);

            return accessToken;
        }

        public string Authenticate(string email, out User user)
        {
            // Get user based on the email provided
            user = _userRepository.GetOne(data => data.Email.Equals(email) &&
                                                  data.Status == CommonStatus.Active);

            // Create user claims use for generating access token
            var claims = new List<Claim>()
                  .AddUserId(user.Id)
                  .AddUserEmail(user.Email);

            // Generate access token for user
            var accessToken = _jwtProvider.GenerateToken(user, claims, DateTimeExtension.GetCurrentDateTimeUtc()
                                                                                        .AddMinutes(15));

            return accessToken;
        }
    }
}
