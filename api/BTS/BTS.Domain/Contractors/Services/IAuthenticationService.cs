using BTS.Domain.Models.Entities;

namespace BTS.Domain.Contractors.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(string userNameOrEmail, string password, out User user);
    }
}
