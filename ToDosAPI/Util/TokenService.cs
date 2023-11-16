using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Util;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(UserWithRolesDto userWithRolesDto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userWithRolesDto.Username!),
            new(ClaimTypes.Name, userWithRolesDto.FullName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
         };
        claims.AddRange(userWithRolesDto.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
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