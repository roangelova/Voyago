using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GetMyTicket.Service.Authorization
{
    public class TokenService
    {
        private static readonly RandomNumberGenerator randomNumberGenerator 
            = RandomNumberGenerator.Create();
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
        }

        public string GenerateAccessToken(string email)
        {
            var jwtSettings = configuration.GetSection("Jwt");

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtSettings["Issuer"],
                jwtSettings["Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpitesInXMinutes"])),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[32]; 
            randomNumberGenerator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }

}
