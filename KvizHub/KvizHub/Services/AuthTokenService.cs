using KvizHub.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KvizHub.Services
{
    public class AuthTokenService : IAuthToken
    {

        private readonly string _secretKey;
        private readonly string _issuer;
        public AuthTokenService(IConfiguration configuration) {
            _secretKey = configuration.GetSection("SecretKey").Value;
            _issuer = configuration.GetSection("TokenIssuer").Value;
        }
        public string CreateToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, username == "admin" ? "admin" : "user")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
