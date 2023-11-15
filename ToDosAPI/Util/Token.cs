using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDosAPI.Models;

namespace ToDosAPI.Util
{
    public class Token
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);


        }
        public List<Claim> GenerateClaims(User user)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user!.Username!, user.FullName, user.UserType.ToString() ),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return authClaims;
        }
        }
}
