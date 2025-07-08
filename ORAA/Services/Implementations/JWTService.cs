using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ORAA.Core;
using ORAA.Models;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public JWTService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public UserToken GetUserToken(User user)
        {
            var jwtKey = _configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key not found in configuration");
            var jwtIssuer = _configuration["JWT:Issuer"] ?? throw new InvalidOperationException("JWT:Issuer not found in configuration");
            var jwtAudience = _configuration["JWT:Audience"] ?? throw new InvalidOperationException("JWT:Audience not found in configuration");
            var jwtDuration = int.Parse(_configuration["JWT:DurationInMinutes"] ?? "300");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                expires: DateTime.Now.AddMinutes(jwtDuration),
                claims: claims,
                signingCredentials: credentials
            );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
        {
            throw new NotImplementedException();
        }
    }
}
