using BTS.Domain.Models.Entities;

namespace BTS.Domain.Contractors.Authentication
{
    public interface IAuthenticationService
    {
        string Authenticate(string userNameOrEmail, string password, out User user);
        string Authenticate(string email, out User user);
    }
}
