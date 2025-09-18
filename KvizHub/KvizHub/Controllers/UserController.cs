using KvizHub.DTO.User;
using KvizHub.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KvizHub.Controllers
{
        [Route("api/users")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost("registration")]
            public IActionResult Register([FromForm] RegistrationDTO registrationData)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string jwtToken = _userService.GetAllUsers(registrationData);
                return Ok(new { Token = jwtToken });
            }

            [HttpPost("login")]
            public IActionResult Authenticate([FromBody] LoginDTO loginData)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string jwtToken = _userService.SignUp(loginData);
                return Ok(new { Token = jwtToken });
            }

            [HttpGet]
            [Authorize(Roles = "admin")]
            public IActionResult GetAll()
            {
                List<string> allUsers = _userService.GetAllUsers();
                return Ok(allUsers);
            }

            [HttpGet("image")]
            [Authorize]
            public IActionResult GetProfilePicture()
            {
                string currentUsername = User?.Identity?.Name;
                string imageFileName = _userService.GetProfileImagePath(currentUsername);

                return Ok(new { ImageName = imageFileName });
            }
        }

    }