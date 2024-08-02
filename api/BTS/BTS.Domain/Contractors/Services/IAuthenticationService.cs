using BTS.Domain.Results;

namespace BTS.Domain.Contractors.Services
{
    public interface IAuthenticationService
    {
        Result Authenticate(string userNameOrEmail, string password);
    }
}
