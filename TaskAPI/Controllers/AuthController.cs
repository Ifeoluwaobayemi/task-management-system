using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models.Dtos;
using TaskAPI.Services.Interfaces;



namespace TaskAPI.Controllers
{
    [Route("api/auth")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
            {
                var newUser = await _authService.RegisterAsync(registerDto);

                if (newUser != null)
                {
                    return Ok(newUser);
                }

                return BadRequest("Registration failed"); 
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
            {
                var token = await _authService.LoginAsync(loginDto);

                if (token != null)
                {
                    return Ok(new { Token = token });
                }

                return Unauthorized("Invalid login credentials"); 
            }
        }
    }
