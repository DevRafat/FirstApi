using Azure.Core;
using FirstApi.Models;
using FirstApi.Service.User;
using FirstApi.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        public readonly IUserService _UserService;  
        public UsersController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult>Post(UserRequestModel m)
        {
            if (m.Password != m.ConfirmPassword)
                return BadRequest("Password Not Matched");
            var token = _UserService.CreateToken(m.UserName);
            return Ok(token);

        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(AuthModel m)
        {
            var user = await _UserService.GetUser(m);
            if (user == null)
                return BadRequest("User Not Found");

            if (!BCrypt.Net.BCrypt.Verify(m.Password, user.Password))
            {
                return BadRequest("Wrong password.");
            }
            var token = _UserService.CreateToken(m.UserName);
            return Ok(token);

        }

    }
}
