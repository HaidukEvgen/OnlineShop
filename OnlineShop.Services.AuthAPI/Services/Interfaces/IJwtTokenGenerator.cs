using OnlineShop.Services.AuthAPI.Models.Data;

namespace OnlineShop.Services.AuthAPI.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
