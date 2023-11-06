using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDosAPI.Data;
using ToDosAPI.Models.UserClasses;
using ToDosAPI.Util;

namespace ToDosAPI.Services;

public class UserServices : IUserServices
{
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher _passwordHasher;
    private readonly UserRepository _userRepo;

    public UserServices(IConfiguration configuration, PasswordHasher passwordHasher, UserRepository userRepo)
    {
        _configuration = configuration;
        _passwordHasher = passwordHasher;
        _userRepo = userRepo;
    }

    public async Task<bool> AddNewUser(User user)
    {
        user.Salt = _passwordHasher.GenerateSalt();
        user.Password = _passwordHasher.HashPassword(user.Password!, user.Salt);
        var createdUser = await _userRepo.CreateUserAsync(user);
        return createdUser is not null;
    }


    private string GenerateToken(IEnumerable<Claim> claims)
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

    public async Task<string?> Login(string username, string password)
    {
        var userInfo = await _userRepo.GetUserAsync(username);

        if (userInfo is null) return null;


        var result = _passwordHasher.CheckPassword(password, userInfo.Password, userInfo.Salt);

        if (result)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, userInfo!.Username!, userInfo.FullName, userInfo.UserType.ToString() ),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return GenerateToken(authClaims);

        }

        return null!;
    }
}