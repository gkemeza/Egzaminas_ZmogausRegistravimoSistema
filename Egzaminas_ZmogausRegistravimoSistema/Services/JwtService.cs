using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Egzaminas_ZmogausRegistravimoSistema.Services
{
    public interface IJwtService
    {
        string GetJwtToken(User user);
    }

    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("Jwt:Key") ?? "";
            _issuer = configuration.GetSection("Jwt:Issuer").Value ?? "";
            _audience = configuration.GetSection("Jwt:Audience").Value ?? "";
        }

        public string GetJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Username),
                new (ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
