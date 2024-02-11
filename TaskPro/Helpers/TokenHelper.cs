using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskPro.Helpers
{
    public class TokenHelper
    {
        public string key;
        public string issuer;
        public string audience;

        private readonly SymmetricSecurityKey securityKey;
        public TokenHelper(string key, string issuer, string audience)
        {
            this.key = key;
            this.issuer = issuer;
            this.audience = audience;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
        public string GenerateToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public string? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var id = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                return id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
