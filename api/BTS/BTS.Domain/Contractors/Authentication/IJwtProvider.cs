using BTS.Domain.Models.Entities;
using System.Security.Claims;

namespace BTS.Domain.Contractors.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(User user, List<Claim> claims, DateTime? expirationDate = null);
        string? GetValueByClaim(string claimName, string token);
    }
}
