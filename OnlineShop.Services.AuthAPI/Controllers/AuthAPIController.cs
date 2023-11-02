using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AuthAPI.Exceptions;
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
            var registerError = await _authService.Register(model);
            if (registerError is not null)
            {
                throw new RegisterException(registerError.Description);
            }

            await _authService.Register(model);
            return Ok(new ResponseDto { Message = "User registered successfuly" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                throw new LoginException("Username or password is incorrect");
            }

            return Ok(new ResponseDto { Message = "User logged in successfuly", Result = loginResponse });
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                throw new AssignRoleException("Error while assigning a role");
            }
            return Ok(new ResponseDto { Message = "Role assigned successfuly" });
        }
    }
}
