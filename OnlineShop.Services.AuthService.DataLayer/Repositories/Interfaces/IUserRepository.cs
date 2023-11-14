using Microsoft.AspNetCore.Identity;
using OnlineShop.Services.AuthService.DataLayer.Models.Data;

namespace OnlineShop.Services.AuthService.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);

        Task<ApplicationUser?> GetByNameAsync(string name);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<IList<string>> GetRolesAsync(ApplicationUser user);

        Task<bool> RoleExistsAsync(string roleName);

        Task CreateRoleAsync(string roleName);

        Task AddToRoleAsync(ApplicationUser user, string roleName);
    }
}
