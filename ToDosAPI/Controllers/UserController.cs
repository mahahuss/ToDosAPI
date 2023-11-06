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
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            this._userService = userService;

        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody] User user)
        {
            var check = await _userService.AddNewUser(user);
            return Ok(check);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var token = await _userService.Login(username, password);
            if (token != "")
            {
                return Ok(token);
            }
            return this.Unauthorized();

        }
    }
}
