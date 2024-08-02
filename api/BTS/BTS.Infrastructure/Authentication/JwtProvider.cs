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
            // Create an instance of claims for specific user
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(Common.CLAIM_NAME_ROLE, user.IsAdmin ? Common.CLAIM_VALUE_ROLE_ADMIN : Common.CLAIM_VALUE_ROLE_USER)
            };

            // Create an instance of signing credentials user for instace of jwt security token
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_setting.SecretKey),
                    SecurityAlgorithms.HmacSha256);

            // Create an instance of jwt security token
            var token = new JwtSecurityToken(
                _setting.Issuer,
                _setting.Audience,
                claims,
                null,
                DateTimeExtension.GetCurrentDateTimeOffsetUtc().AddHours(1).DateTime,
                signingCredentials);

            // Generate token value
            var tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }
    }
}
