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
        _imageDir = configuration.GetValue<string>("Files:ImagesPath")!;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto user)
    {
        var userInfo = await _userService.AddNewUserAsync(user);

        if (userInfo is not null)return Ok("User registered successfully");

        return BadRequest("Failed to register");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var token = await _userService.LoginAsync(loginDto.Username, loginDto.Password);

        if (!string.IsNullOrEmpty(token))
        {
            return Ok(new
            {
                token
            });
        }

        return Unauthorized("Username or password incorrect");
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserPhoto(int userId)
    {
        var imagePath = Path.Combine(_imageDir!, userId + ".png");
        if (!System.IO.File.Exists(imagePath)) return NotFound();

        var photoByte = await System.IO.File.ReadAllBytesAsync(imagePath);

        if (photoByte.Length > 0)
        {
            return File(photoByte, "image/png");
        }

        return NotFound();
    }


    [HttpPut]
    public async Task<ActionResult> EditProfile(UserProfile userProfile)
    {
        var currentUserId = User.GetId();
        var check = await _userService.EditProfileAsync(userProfile, currentUserId);
         if (check) return Ok("Profile updated successfully");

        return BadRequest("Failed to update profile");

    }
}