using InvoiceApp.API.DTOs.AuthDTOs;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace InvoiceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("check-authorize")]
        [Authorize]
        public IActionResult IsBearerAuthorizedAsync()
        {
            return Ok(new { message = "This is a protected method. You have authorized with bearer." });
        }

        // Login Section
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dto)
        {
            var loginResponse = await _authService.LoginAsync(dto);

            var token = loginResponse.Token;

            return Ok(new { message = "Login successful", token });
        }

        // Register Section
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
        {
            await _authService.RegisterAsync(dto);

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("get-user-summary")]
        [Authorize]
        public async Task<IActionResult> GetProtectedDataAsync([FromBody] JwtRequestDTO dto)
        {
            if (string.IsNullOrEmpty(dto.JwtTokenString))
                return Unauthorized(new { message = "Token is missing or invalid" });

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(dto.JwtTokenString);

            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            var oldUser = await _authService.CheckUserNotFoundAsync(id);

            var data = new
            {
                message = "This is protected data",
                userId = id,
                role = role,
                username = oldUser.UserName,
                userEmail = oldUser.Email,
                fullName = oldUser.FullName,
            };

            return Ok(data);
        }
    }
}
