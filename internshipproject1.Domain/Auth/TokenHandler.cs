using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;


namespace internshipproject1.Domain.Auth
{
    public class TokenHandler
    {
        // RBAC destekli overload
        public static Token CreateToken(
            IConfiguration configuration,
            string userName,
            int userId,
            List<string> roles,
            Dictionary<string, string>? additionalClaims = null)
        {
            Token token = new();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("userId", userId.ToString()),
                new Claim("username", userName)
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                if (roles.Count > 0)
                {
                    claims.Add(new Claim("primaryRole", roles[0]));
                    claims.Add(new Claim("roleCount", roles.Count.ToString()));
                }
            }

            if (additionalClaims != null)
            {
                foreach (var kv in additionalClaims)
                {
                    claims.Add(new Claim(kv.Key, kv.Value));
                }
            }

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                claims: claims,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            token.RefreshToken = Convert.ToBase64String(numbers);
            return token;
        }

        // Backward compatibility için eski method'u koru
        public static Token CreateToken(IConfiguration configuration, string userName)
        {
            return CreateToken(configuration, userName, 0, new List<string>());
        }
    }
}
