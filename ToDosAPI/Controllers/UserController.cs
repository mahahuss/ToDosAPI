using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using ToDosAPI.Models;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;

        public UserController(UserServices userService)
        {
            _userService = userService;

        }

        [HttpPost]
        public async Task<ActionResult> AddNewUser([FromBody] User user)
        {
            var userInfo = await _userService.AddNewUser(user);
            return Ok(userInfo);
        }

        [HttpGet]
        public async Task<ActionResult> Login(string username, string password)
        {
            var token = await _userService.Login(username, password);
            if (string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            return Unauthorized();

        }
    }
}
