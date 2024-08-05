using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Domain.Contractors.Authentication
{
    public interface IAuthenticationService
    {
        string AuthenticateByCredentials(string userNameOrEmail, string password, out User user);
        string? AuthenticateByEmail(string email, AccessTokenType tokenType, out User user);
        string AuthenticateByToken(string email, out User user);
    }
}
