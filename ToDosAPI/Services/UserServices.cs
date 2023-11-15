using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDosAPI.Data;
using ToDosAPI.Models;
using ToDosAPI.Util;

namespace ToDosAPI.Services;

public class UserServices
{
   
    private readonly PasswordHasher _passwordHasher;
    private readonly UserRepository _userRepo;
    private readonly Token _userToken;


    public UserServices( PasswordHasher passwordHasher, UserRepository userRepo, Token userToken)
    {
        _passwordHasher = passwordHasher;
        _userRepo = userRepo;
        _userToken = userToken;
    }

    public async Task<User?> AddNewUser(User user)
    {
        user.Salt = _passwordHasher.GenerateSalt();
        user.Password = _passwordHasher.HashPassword(user.Password!, user.Salt);
        var createdUser = await _userRepo.CreateUserAsync(user);
        return createdUser;
    }

    public async Task<string?> Login(string username, string password)
    {
        var userInfo = await _userRepo.GetUserAsync(username);
        if (userInfo is null) return null;
        var result = _passwordHasher.CheckPassword(password, userInfo.Password, userInfo.Salt);
        if (result)
        {
            return _userToken.GenerateToken(_userToken.GenerateClaims(userInfo));
        }
        return null!;
    }
}