using Microsoft.IdentityModel.Logging;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using ToDosAPI.Data;
using ToDosAPI.Models;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

public class UserService
{
    private readonly PasswordHasherService _passwordHasherService;
    private readonly UserRepository _userRepo;
    private readonly TokenService _userToken;
    private readonly string _imageDir;
    private readonly FileService _fileService;


    public UserService(PasswordHasherService passwordHasherService, UserRepository userRepo, TokenService userToken,
        IConfiguration configuration, FileService fileService)
    {
        _passwordHasherService = passwordHasherService;
        _userRepo = userRepo;
        _userToken = userToken;
        _imageDir = configuration.GetValue<string>("Files:ImagesPath")!;
        _fileService = fileService;
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

    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        var userCredential = await _userRepo.GetUserCredentialsAsync(username);

        if (userCredential is null) return Result<string>.Failure("User was not found");

        var result = _passwordHasherService.CheckPassword(password, userCredential.Password!, userCredential.Salt!);

        if (!result) return Result<string>.Failure("Username or password incorrect");

        var userInfo = await _userRepo.GetUserWithRolesAsync(username);
        return userInfo != null && userInfo.Status
            ? _userToken.GenerateToken(userInfo!)
            : Result<string>.Failure("Failed to generate token");
    }

    public async Task<bool> EditProfileAsync(UpdateUserProfileDto updateUserInfo, int id)


    {
        if (updateUserInfo.Image != null && updateUserInfo.Image.Length < 200000 &&
            _fileService.CheckContentType(updateUserInfo.Image.FileName) == "image/png")
        {
            var filename = id.ToString() + System.IO.Path.GetExtension(updateUserInfo.Image.FileName);
            await using var fileStream = new FileStream(Path.Combine(_imageDir, filename), FileMode.Create);
            await updateUserInfo.Image.CopyToAsync(fileStream);
        }

        var check = await _userRepo.EditUserProfileAsync(updateUserInfo.Name, id);

        return check;
    }

    public async Task<List<GetUsers>> GetUsersAsync(int currentUserId)
    {
        return await _userRepo.GetUsersAsync(currentUserId);
    }

    public async Task<bool> ChangeUserStatusAsync(int userId, bool status)
    {
        return await _userRepo.ChangeUserStatusAsync(userId, status);
    }

    public async Task<string> RefreshToken(string username)
    {
        var userInfo = await _userRepo.GetUserWithRolesAsync(username);
        return _userToken.GenerateToken(userInfo!);
    }

    public async Task<List<Role>> GetUserRolesAsync(int userId)
    {
        return await _userRepo.GetUserRolesAsync(userId);
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await _userRepo.GetAllRolesAsync();
    }

    public async Task<bool> EditProfileByAdminAsync(EditProfileByAdminDto editProfileByAdminDto)
    {
        var check = await _userRepo.EditUserProfileAsync(editProfileByAdminDto);
        return check;
    }

    public async Task<List<UserToShareWith>> GetSharedWithAsync(int userId, int taskId)
    {
        return await _userRepo.GetSharedWithAsync(userId, taskId);
    }
}