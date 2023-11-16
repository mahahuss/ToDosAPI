using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDosAPI.Models.Dtos;

namespace ToDosAPI.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public class ClaimTest
    {
        const string nameId = "nameId";
        const string nameIdentifier = "https:/......";

        public ClaimTest(string claimName)
        {
            if (claimName == nameId)
                ClaimType = nameIdentifier;
        }

        public string ClaimType { get; set; }
    }

    public string GenerateToken(UserWithPasswordAndRolesDto userWithRolesDto)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, userWithRolesDto.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userWithRolesDto.Username!),
            new(JwtRegisteredClaimNames.GivenName, userWithRolesDto.FullName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(userWithRolesDto.Roles.Select(role => new Claim("roles", role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}