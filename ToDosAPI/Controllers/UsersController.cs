using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;
    private readonly string _imageDir;

    public UsersController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
        _imageDir= _configuration.GetValue<string>("Files:ImagesPath")!;
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
        if (System.IO.File.Exists(Path.Combine(_imageDir!, userId + ".png")))
        {
         var photoByte = await System.IO.File.ReadAllBytesAsync(Path.Combine(_imageDir!, userId + ".png"));

            if (photoByte.Length > 0)
            {
                return File(photoByte, "image/png");
            }
        }
        return NotFound();
    }
}