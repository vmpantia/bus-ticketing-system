using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Exceptions;
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

        /// <summary>
        /// Authenticate using user credentials
        /// </summary>
        /// <param name="userNameOrEmail"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>Access Token</returns>
        public string AuthenticateByCredentials(string userNameOrEmail, string password, out User user)
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

        /// <summary>
        /// Authenticate using email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="user"></param>
        /// <returns>Access Token</returns>
        public string AuthenticateByEmail(string email, out User user)
        {
            // Get user based on the email provided
            user = _userRepository.GetOne(data => data.Email.Equals(email) &&
                                                  data.Status == CommonStatus.Active);

            // Create user claims use for generating access token
            var claims = new List<Claim>()
                  .AddUserId(user.Id)
                  .AddUserEmail(user.Email)
                  .AddTokenType(AccessTokenType.MagicLink);

            // Generate access token for user
            var accessToken = _jwtProvider.GenerateToken(user, claims, DateTimeExtension.GetCurrentDateTimeUtc()
                                                                                        .AddMinutes(15));

            return accessToken;
        }

        /// <summary>
        /// Authenticate using token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns>Access Token</returns>
        public string AuthenticateByToken(string token, out User user)
        {
            // Get necessary values from the token
            var userId = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_ID, token);
            var userEmail = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_EMAIL, token);
            var tokenType = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_TOKEN_TYPE, token);

            // Check if the userId is valid Guid
            if (!Guid.TryParse(userId, out Guid id))
                throw new ArgumentException("User Id from token is not a valid user id.");

            // Check if the userEmail is valid
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentException("User email from token must have value.");

            // Check if the token type is valid
            if (string.IsNullOrEmpty(tokenType))
                throw new ArgumentException("Access Token Type from token must have value.");

            // Check if the user exist in the database
            if (!_userRepository.IsExist(data => data.Id == id &&
                                                data.Email == userEmail &&
                                                data.Status == CommonStatus.Active, out user))
                throw new NotFoundException(string.Format(ErrorMessage.ERROR_NOT_FOUND_FORMAT, nameof(User)));

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
    }
}
