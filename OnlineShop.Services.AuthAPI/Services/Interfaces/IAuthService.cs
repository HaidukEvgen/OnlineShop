using Microsoft.AspNetCore.Identity;
using OnlineShop.Services.AuthAPI.Models.Dto;

namespace OnlineShop.Services.AuthAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityError?> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
