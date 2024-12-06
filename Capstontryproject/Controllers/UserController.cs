using Capstontryproject.Dtos;
using Capstontryproject.Servses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstontryproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // تسجيل مستخدم جديد
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDto)
        {
            var result = await _userService.SignUpAsync(userDto);

            if (result.Success)
            {
                return Ok(result.Data);  // Return the created user data if successful
            }
            else
            {
                return BadRequest(new { message = result.Message });  // Return an error message if failed
            }
        }

        // تسجيل الدخول
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userDto)
        {
            var result = await _userService.LoginAsync(userDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }
    }
}
