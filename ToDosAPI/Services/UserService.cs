using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

public class UserService
{
    private readonly PasswordHasherService _passwordHasherService;
    private readonly UserRepository _userRepo;
    private readonly TokenService _userToken;


    public UserService(PasswordHasherService passwordHasherService, UserRepository userRepo, TokenService userToken)
    {
        _passwordHasherService = passwordHasherService;
        _userRepo = userRepo;
        _userToken = userToken;
    }

    public async Task<UserWithRolesDto?> AddNewUserAsync(RegisterDto registerDto)
    {
        var salt = _passwordHasherService.GenerateSalt();
        var password = _passwordHasherService.HashPassword(registerDto.Password!, salt);
        var createdUser = await _userRepo.CreateUserAsync(new AppUser
        {
            FullName = registerDto.FullName,
            Password = password,
            Salt = salt,
            Username = registerDto.Username
        }, 3);

        if (createdUser is not null)
        {
            return await _userRepo.GetUserWithRolesAsync(registerDto.Username);
        }

        return null;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var userCredential = await _userRepo.GetUserCredentialsAsync(username);

        if (userCredential is null) return null;

        var result = _passwordHasherService.CheckPassword(password, userCredential.Password!, userCredential.Salt!);
        if (!result) return null;

        var userInfo = await _userRepo.GetUserWithRolesAsync(username);
        return userInfo != null ? _userToken.GenerateToken(userInfo!) : null;
    }
}