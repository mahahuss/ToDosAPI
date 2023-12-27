using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDosAPI.Extensions;
using ToDosAPI.Models;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;
    private readonly string _imageDir;
    private readonly FileService _fileService;

    public UsersController(UserService userService, IConfiguration configuration, FileService fileService)
    {
        _userService = userService;
        _imageDir = configuration.GetValue<string>("Files:ImagesPath") ??
                    throw new Exception("Configuration Files:ImagesPath not found");
        _fileService = fileService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto user)
    {
        var userInfo = await _userService.AddNewUserAsync(user);

        if (userInfo is not null) return Ok("User registered successfully");

        return BadRequest("Failed to register");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var result = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
        return result.Match<ActionResult>(Ok, Unauthorized);
    }

    [HttpGet("images/{userId}")]
    public ActionResult GetUserPhoto(int userId)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDir, userId + ".png");

        if (!System.IO.File.Exists(imagePath)) return NotFound();

        return PhysicalFile(imagePath, _fileService.CheckContentType(imagePath));
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("userRoles/{userId}")]
    public async Task<ActionResult> GetRoles(int userId)
    {
        var roles = await _userService.GetUserRolesAsync(userId);
        return Ok(roles);
    }

    [HttpGet("roles")]
    public async Task<ActionResult> GetallRoles()
    {
        var roles = await _userService.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("token")]
    public ActionResult RefreshToken()
    {
        var token = _userService.RefreshToken(User.GetId(), User.GetUsername()!, User.GetFullname()!, User.GetRoles());
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }

    [HttpPut]
    public async Task<ActionResult> EditProfile([FromForm] UpdateUserProfileDto updateUserProfileDto)
    {
        var currentUserId = User.GetId();
        var check = await _userService.EditProfileAsync(updateUserProfileDto, currentUserId);
        if (check) return Ok("Profile updated successfully");

        return BadRequest("Failed to update profile");
    }

    [HttpPut("editRoles")]
    public async Task<ActionResult> EditProfileByAdmin(EditProfileByAdminDto editProfileByAdminDto)
    {
        var check = await _userService.EditProfileByAdminAsync(editProfileByAdminDto);
        if (check) return Ok("Profile updated successfully");

        return BadRequest("Failed to update profile");
    }

    [HttpPut("status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangeUserStatus(ChangeUserStatusDto changeUserStatusDto)
    {
        var check = await _userService.ChangeUserStatusAsync(changeUserStatusDto.userId, changeUserStatusDto.status);
        if (check) return Ok("User status changed successfully");

        return BadRequest("Failed to change user status");
    }
}