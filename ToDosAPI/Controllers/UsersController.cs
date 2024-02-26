﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDosAPI.Extensions;
using ToDosAPI.Models;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

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
        var userInfo = await _userService.AddNewUserAsync(user);

        if (userInfo is not null) return Ok("User registered successfully");

        return BadRequest("Failed to register");
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
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDir, userId + ".png");

        if (!System.IO.File.Exists(imagePath)) return NotFound();

        Byte[] bytes = System.IO.File.ReadAllBytes(imagePath);
        String file = Convert.ToBase64String(bytes);
        var image = new ProfileImage
        {
            FileBase64 = file
        };
        return Ok(image);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<ActionResult> GetUsers()
    {
        var currentUserId = User.GetId();
        //var roles = User.GetRoles(); // send as a parameters

        //if (!roles.Contains("Admin") && !roles.Contains("Moderator"))
        //    return Unauthorized("Unauthorized: due to invalid credentials");
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
        var token = await _userService.RefreshToken(User.GetUsername()!);
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
        var result = await _userService.EditProfileAsync(updateUserProfileDto, currentUserId);
        if (result) return Ok("Profile updated successfully");

        return BadRequest("Failed to update profile");
    }

    [HttpPut("edit-roles")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> EditProfileByAdmin(EditProfileByAdminDto editProfileByAdminDto)
    {
        if(editProfileByAdminDto.Roles.Count ==0) return BadRequest("Failed to update profile");

        var result = await _userService.EditProfileByAdminAsync(editProfileByAdminDto);
        if (result) return Ok("Profile updated successfully");

        return BadRequest("Failed to update profile");
    }

    [HttpPut("status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangeUserStatus(ChangeUserStatusDto changeUserStatusDto)
    {
        var result = await _userService.ChangeUserStatusAsync(changeUserStatusDto.userId, changeUserStatusDto.status);
        if (result) return Ok("User status changed successfully");

        return BadRequest("Failed to change user status");
    }
}