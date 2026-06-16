using DoctorAppointmentSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorAppointmentSystem.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration Configuration;
        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GenerateToken(Guid id, string email, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JwtSettings:Issuer"],
                audience: Configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(Configuration["JwtSettings:ExpiryInHours"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
