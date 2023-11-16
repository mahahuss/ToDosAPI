using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;
using ToDosAPI.Util;

namespace ToDosAPI.Services;

public class UserService
{

    private readonly PasswordHasher _passwordHasher;
    private readonly UserRepository _userRepo;
    private readonly TokenService _userToken;


    public UserService(PasswordHasher passwordHasher, UserRepository userRepo, TokenService userToken)
    {
        _passwordHasher = passwordHasher;
        _userRepo = userRepo;
        _userToken = userToken;
    }

    public async Task<UserWithRolesDto?> AddNewUserAsync(RegisterDto registerDto)
    {
        var salt = _passwordHasher.GenerateSalt();
        var password = _passwordHasher.HashPassword(registerDto.Password!, salt);
        var createdUser = await _userRepo.CreateUserAsync(new User
        {
            FullName = registerDto.FullName,
            Password = password,
            Salt = salt,
            Username = registerDto.Username
        }, 3);

        if (createdUser is not null) return await _userRepo.GetUserWithRolesAsync(registerDto.Username, "register");

        return null;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var userInfo = await _userRepo.GetUserWithRolesAsync(username, "login");

        if (userInfo is null) return null;

        var result = _passwordHasher.CheckPassword(password, userInfo.Password!, userInfo.Salt!);

        return result ? _userToken.GenerateToken(userInfo) : null;
    }

}