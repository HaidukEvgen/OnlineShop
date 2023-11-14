using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AuthService.BusinessLayer.Models.Dto;
using OnlineShop.Services.AuthService.BusinessLayer.Services.Interfaces;

namespace OnlineShop.Services.AuthService.Api.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var response = await _authService.RegisterAsync(model);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = await _authService.LoginAsync(model);

            return Ok(response);
        }

        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto model)
        {
            var response = await _authService.AssignRoleAsync(model.Email, model.Role.ToUpper());

            return Ok(response);
        }
    }
}
