using Microsoft.IdentityModel.Logging;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Api.Data;
using Api.Models;
using Api.Models.Dtos;
using Api.Models.Entities;

namespace Api.Services;

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

    public async Task<Result<UserWithRolesDto?>> AddNewUserAsync(RegisterDto registerDto)
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

        return createdUser != null ? await _userRepo.GetUserWithRolesAsync(registerDto.Username) : Result<UserWithRolesDto?>.Failure("Failed to register");

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

    public async Task<Result<string>> EditProfileAsync(UpdateUserProfileDto updateUserInfo, int id)
    {
        if (updateUserInfo.Image != null && updateUserInfo.Image.Length < 200000 &&
            _fileService.CheckContentType(updateUserInfo.Image.FileName) == "image/png")
        {
            var filename = id.ToString() + System.IO.Path.GetExtension(updateUserInfo.Image.FileName);
            await using var fileStream = new FileStream(Path.Combine(_imageDir, filename), FileMode.Create);
            await updateUserInfo.Image.CopyToAsync(fileStream);
        }

        var result = await _userRepo.EditUserProfileAsync(updateUserInfo.Name, id);

        return result? Result<string>.Successful("Profile updated successfully") : Result<string>.Failure("Failed to update profile");
    }

    public Task<List<GetUsers>> GetUsersAsync(int currentUserId)
    {
        return _userRepo.GetUsersAsync(currentUserId);
    }

    public async Task<Result<string>> ChangeUserStatusAsync(int userId, bool status)
    {
        var result = await _userRepo.ChangeUserStatusAsync(userId, status);
        return result? Result<string>.Successful("User status changed successfully") : Result<string>.Failure("Failed to change user status");
    }

    public async Task<Result<string>> RefreshToken(string username)
    {
        var userInfo = await _userRepo.GetUserWithRolesAsync(username);
        if (userInfo == null) Result<string>.Failure("Failed to retrieve user information");
        var token = _userToken.GenerateToken(userInfo!);
        return token == null ? Result<string>.Failure("Failed to generate token") : token;
    }

    public async Task<List<Role>> GetUserRolesAsync(int userId)
    {
        return await _userRepo.GetUserRolesAsync(userId);
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await _userRepo.GetAllRolesAsync();
    }

    public async Task<Result<string>> EditProfileByAdminAsync(EditProfileByAdminDto editProfileByAdminDto)
    {
        if (editProfileByAdminDto.Roles.Count == 0) return Result<string>.Failure("Failed to update profile");
        var check = await _userRepo.EditUserProfileAsync(editProfileByAdminDto);
        return check? Result<string>.Successful("Profile updated successfully"): Result<string>.Failure("Failed to update profile");
    }
    public Task<List<UsersToShareDto>> GetUserstoShareAsync(int currentUserId)
    {
        return _userRepo.GetUserstoShareAsync(currentUserId);
    }

    public Result<ProfileImage?> GetUserPhoto(int userId)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDir, userId + ".png");

        if (!System.IO.File.Exists(imagePath)) return Result<ProfileImage?>.Failure("Failed to generate token");

        Byte[] bytes = System.IO.File.ReadAllBytes(imagePath);
        String file = Convert.ToBase64String(bytes);
        var image = new ProfileImage
        {
            FileBase64 = file
        };

        return image;
    }
}