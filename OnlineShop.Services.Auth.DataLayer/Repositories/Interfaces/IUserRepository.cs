using Microsoft.AspNetCore.Identity;
using OnlineShop.Services.Auth.DataLayer.Models.Data;

namespace OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);

        Task<ApplicationUser?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<IList<string>> GetRolesAsync(ApplicationUser user);

        Task<bool> RoleExistsAsync(string roleName);

        Task CreateRoleAsync(string roleName);

        Task AddToRoleAsync(ApplicationUser user, string roleName);
    }
}
