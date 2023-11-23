using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public UsersController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto user)
    {
        var userInfo = await _userService.AddNewUserAsync(user);

        if (userInfo is not null) return Ok();

        return BadRequest();
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
        var imagesDir =
            _configuration.GetValue<string>("Files:ImagesPath"); // move to constructor and assign to a readonly field
        var photoByte =
            await System.IO.File.ReadAllBytesAsync(Path.Combine(imagesDir!, userId + ".png")); // check file exists

        if (photoByte.Length > 0) return File(photoByte, "image/png");

        return NotFound();
    }
}