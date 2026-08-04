using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBudgett.Core.Security.Abstract;
using SmartBudgett.Entities;


namespace SmartBudgett.Core.Security.Concrete
{
    public class JwtHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var securityKey = jwtSettings["SecurityKey"];

            if (string.IsNullOrWhiteSpace(securityKey))
                throw new InvalidOperationException("JWT security key is not configured.");

            var expirationMinutes = int.TryParse(
                jwtSettings["AccessTokenExpirationMinutes"],
                out var configuredExpirationMinutes)
                ? configuredExpirationMinutes
                : 120;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.FirstName ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
