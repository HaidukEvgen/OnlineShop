using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRoleAsync(string email, string roleName);
    }
}
