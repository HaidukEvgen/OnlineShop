using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<UserDto>> RegisterAsync(RegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken = default);
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default);
        Task<ResponseDto> AssignRoleAsync(string email, string roleName, CancellationToken cancellationToken = default);
    }
}
