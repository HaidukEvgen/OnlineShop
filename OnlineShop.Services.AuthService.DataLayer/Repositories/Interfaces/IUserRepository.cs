using OnlineShop.Services.AuthService.DataLayer.Models.Data;

namespace OnlineShop.Services.AuthService.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
