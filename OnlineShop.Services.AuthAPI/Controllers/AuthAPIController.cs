using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AuthAPI.Models.Dto;
using OnlineShop.Services.AuthAPI.Services.Interfaces;

namespace OnlineShop.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var response = await _authService.RegisterAsync(model);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = await _authService.LoginAsync(model);
            return Ok(response);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto model)
        {
            var response = await _authService.AssignRoleAsync(model.Email, model.Role.ToUpper());
            return Ok(response);
        }
    }
}
