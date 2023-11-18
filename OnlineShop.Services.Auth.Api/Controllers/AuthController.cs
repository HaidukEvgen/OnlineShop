using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;

namespace OnlineShop.Services.Auth.Api.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto model, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto model, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleRequestDto model, CancellationToken cancellationToken)
        {
            var response = await _authService.AssignRoleAsync(model.Name, model.Role.ToUpper(), cancellationToken);

            return Ok(response);
        }
    }
}
