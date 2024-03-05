using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Api.Extensions;
using Core.Dtos;
using Infrastructure.Services;

namespace Api.Controllers;

[Authorize]
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto user)
    {
        var result = await _userService.AddNewUserAsync(user);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var result = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
        return result.Match<ActionResult>(Ok, Unauthorized);
    }

    [HttpGet("image")]
    public ActionResult GetUserPhoto()
    {
        var userId = User.GetId();
        var result = _userService.GetUserPhoto(userId);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<ActionResult> GetUsers()
    {
        var currentUserId = User.GetId();
        var users = await _userService.GetUsersAsync(currentUserId);
        return Ok(users);
    }

    [HttpGet("users-to-share")]
    public async Task<ActionResult> GetUserstoShare()
    {
        var currentUserId = User.GetId();
        var users = await _userService.GetUserstoShareAsync(currentUserId);
        return Ok(users);
    }

    [HttpGet("user-roles/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetRoles(int userId)
    {
        var roles = await _userService.GetUserRolesAsync(userId);
        return Ok(roles);
    }

    [HttpGet("roles")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetallRoles()
    {
        var roles = await _userService.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("token")]
    public async Task<ActionResult> RefreshToken()
    {
        var result = await _userService.RefreshToken(User.GetUsername()!);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [HttpPut]
    public async Task<ActionResult> EditProfile([FromForm] UpdateUserProfileDto updateUserProfileDto)
    {
        var currentUserId = User.GetId();
        var result = await _userService.EditProfileAsync(updateUserProfileDto, currentUserId);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [HttpPut("edit-roles")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> EditProfileByAdmin(EditProfileByAdminDto editProfileByAdminDto)
    {
        var result = await _userService.EditProfileByAdminAsync(editProfileByAdminDto);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [HttpPut("status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangeUserStatus(ChangeUserStatusDto changeUserStatusDto)
    {
        var result = await _userService.ChangeUserStatusAsync(changeUserStatusDto.userId, changeUserStatusDto.status);
        return result.Match<ActionResult>(Ok, BadRequest);
    }
}