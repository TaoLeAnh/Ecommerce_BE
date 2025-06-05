using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validate user credentials
            var user = await _userService.ValidateUserAsync(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate token
            var token = _tokenService.GenerateToken(user.UserId.ToString(), user.Role);
            return Ok(new { Token = token , Users = new { user.UserId, user.Email, user.Role } });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Check if user already exists
            var existingUser = await _userService.GetUserByUsernameAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            // Create new user
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password,
                Address = string.Empty,
                Role = "User",
                FullName = string.Empty,
                Phone = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            await _userService.CreateUserAsync(user);

            // Generate token for auto login
            var token = _tokenService.GenerateToken(user.UserId.ToString(), user.Role);

            return Ok(new 
            { 
                Token = token,
                User = new { user.UserId, user.Email, user.Role },
                Message = "Registration successful" 
            });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest 
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}