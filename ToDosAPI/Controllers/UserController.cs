using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;


public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;

    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto user)
    {
        var userInfo = await _userService.AddNewUserAsync(user);
        return Ok(userInfo);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var token = await _userService.LoginAsync(loginDto.Username, loginDto.Password);

        if (!string.IsNullOrEmpty(token))
        {
            return Ok(new
            {
                Token = token
            });
        }

        return Unauthorized();

    }
}