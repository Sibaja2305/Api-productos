using EjercicioProductos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EjercicioProductos.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(UserCredentials user, string role = "user")
        {
            IConfiguration jwtSettings = _configuration.GetSection("JwtSettings");

            Claim[] claims = new Claim[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
            SymmetricSecurityKey key = new SymmetricSecurityKey(keyBytes);
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            JwtSecurityTokenHandler tokenHandler = new();
            string jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
