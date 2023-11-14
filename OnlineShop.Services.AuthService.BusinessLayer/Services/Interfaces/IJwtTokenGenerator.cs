using OnlineShop.Services.AuthService.DataLayer.Models.Data;

namespace OnlineShop.Services.AuthService.BusinessLayer.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
