using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserServices UserSer;
        public UserController(IUserServices UserSer)
        {
            this.UserSer = UserSer;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var Check = await UserSer.AddNewUser(user);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }

        [HttpGet("Login/{username}/{password}")]
        public async Task<IActionResult> Login(String username, String password)
        {
            var Check = await UserSer.login(username, password);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return NotFound();

        }
    }
}
