using BTS.Domain.Constants;
using System.Security.Claims;

namespace BTS.Domain.Extensions
{
    public static class ClaimExtension
    {
        public static List<Claim> AddUserId(this List<Claim> claims, Guid userId)
        {
            claims.Add(new Claim(Common.CLAIM_NAME_USER_ID, userId.ToString()));
            return claims;
        }

        public static List<Claim> AddUserName(this List<Claim> claims, string firstName, string lastName)
        {
            claims.Add(new Claim(Common.CLAIM_NAME_USER_NAME, $"{firstName} {lastName}"));
            return claims;
        }

        public static List<Claim> AddUserEmail(this List<Claim> claims, string email)
        {
            claims.Add(new Claim(Common.CLAIM_NAME_USER_EMAIL, email));
            return claims;
        }

        public static List<Claim> AddUserRole(this List<Claim> claims, bool isAdmin)
        {
            claims.Add(new Claim(Common.CLAIM_NAME_USER_ROLE, isAdmin ? Common.CLAIM_VALUE_ROLE_ADMIN : Common.CLAIM_VALUE_ROLE_USER));
            return claims;
        }
    }
}
