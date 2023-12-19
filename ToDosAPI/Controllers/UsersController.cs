using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Extensions;
using ToDosAPI.Models;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;
    private readonly string _imageDir;

    public UsersController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _imageDir = configuration.GetValue<string>("Files:ImagesPath") ??
                    throw new Exception("Configuration Files:ImagesPath not found");
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
        var token = await _userService.LoginAsync(loginDto.Username, loginDto.Password);

        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized("Username or password incorrect");
    }

    [HttpGet("images/{userId}")]
    public ActionResult GetUserPhoto(int userId)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDir, userId + ".png");

        if (!System.IO.File.Exists(imagePath)) return NotFound();

        return PhysicalFile(imagePath, "image/png");
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPut]
    public async Task<ActionResult> EditProfile([FromForm] UpdateUserProfileDto updateUserProfileDto)
    {
        var currentUserId = User.GetId();
        var check = await _userService.EditProfileAsync(updateUserProfileDto, currentUserId);
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