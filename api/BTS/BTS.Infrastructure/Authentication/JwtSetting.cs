using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BTS.Infrastructure.Authentication
{
    public class JwtSetting
    {
        private const string SectionName = "Jwt";
        private JwtSetting(string issuer, string audience, byte[] secretKey)
        {
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
        }

        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public byte[] SecretKey { get; private set; }

        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
        };

        public static JwtSetting FromConfiguration(IConfiguration configuration)
        {
            var issuer = configuration[$"{SectionName}:{nameof(Issuer)}"]!;
            var audience = configuration[$"{SectionName}:{nameof(Audience)}"]!;
            var secretKey = configuration[$"{SectionName}:{nameof(SecretKey)}"]!;
            byte[] key = Encoding.Unicode.GetBytes(secretKey);

            return new(issuer, audience, key);
        }
    }
}
