using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BTS.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSetting _setting;
        public JwtProvider(JwtSetting setting) => _setting = setting;

        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create an instance of claims for specific user
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(Common.CLAIM_NAME_USER_ROLE, user.IsAdmin ? Common.CLAIM_VALUE_ROLE_ADMIN : Common.CLAIM_VALUE_ROLE_USER)
            };

            // Create an instance of signing credentials user for instace of jwt security token
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_setting.SecretKey),
                    SecurityAlgorithms.HmacSha256);

            // Create an instance of jwt security token
            var securityToken = new JwtSecurityToken(
                issuer: _setting.Issuer,
                audience: _setting.Audience,
                claims: claims,
                expires: DateTimeExtension.GetCurrentDateTimeOffsetUtc().AddHours(1).DateTime,
                signingCredentials: signingCredentials);

            // Generate token value
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }

        public string? GetValueByClaim(string claimName, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var securityToken = (JwtSecurityToken)handler.ReadToken(token);

            return securityToken.Claims.FirstOrDefault(c => c.Type.Equals(claimName))?.Value;
        }
    }
}
