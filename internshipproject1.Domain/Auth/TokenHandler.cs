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
        public static Token CreateToken(IConfiguration configuration, string userName) {

            
            Token token = new();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));


            var claims = new[] {

                new Claim(ClaimTypes.Name, userName)
            };

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
    }
}
