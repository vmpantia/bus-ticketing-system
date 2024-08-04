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
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public JwtProvider(JwtSetting setting)
        {
            _setting = setting;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateToken(User user, List<Claim> claims, DateTime? expirationDate = null)
        {
            // Check if the claims is null or empty
            if (claims.IsNullOrEmpty()) 
                throw new ArgumentNullException("Identity claims for access token cannot be NULL or empty.");

            // Create an instance of signing credentials user for instace of jwt security token
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_setting.SecretKey),
                    SecurityAlgorithms.HmacSha256);

            // Create an instance of jwt security token
            var securityToken = new JwtSecurityToken(
                issuer: _setting.Issuer,
                audience: _setting.Audience,
                claims: claims,
                expires: expirationDate ?? DateTimeExtension.GetCurrentDateTimeUtc()
                                                            .AddHours(1),
                signingCredentials: signingCredentials);

            // Generate token value
            var accessToken = _tokenHandler.WriteToken(securityToken);

            return accessToken;
        }

        public string? GetValueByClaim(string claimName, string token)
        {
            // Decode the JWT token
            var jsonToken = _tokenHandler.ReadToken(token);
            var securityToken = (JwtSecurityToken)_tokenHandler.ReadToken(token);

            return securityToken.Claims.FirstOrDefault(c => c.Type.Equals(claimName))?.Value;
        }

        public async Task<bool> IsTokenValid(string token)
        {
            var result =  await _tokenHandler.ValidateTokenAsync(token, _setting.TokenValidationParameters);
            return result.IsValid;
        }
    }
}
