using OnlineShop.Services.Auth.DataLayer.Models.Data;

namespace OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
