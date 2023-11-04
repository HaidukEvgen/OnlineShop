using OnlineShop.Services.AuthAPI.Models.Data;

namespace OnlineShop.Services.AuthAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
