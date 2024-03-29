﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

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
            new(JwtRegisteredClaimNames.NameId, userWithRolesDto.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userWithRolesDto.Username),
            new(JwtRegisteredClaimNames.GivenName, userWithRolesDto.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(userWithRolesDto.Roles.Select(role => new Claim("roles", role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}