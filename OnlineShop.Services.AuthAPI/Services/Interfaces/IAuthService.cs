using OnlineShop.Services.AuthAPI.Models.Dto;

namespace OnlineShop.Services.AuthAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRoleAsync(string email, string roleName);
    }
}
