using Microsoft.AspNetCore.Identity;
using OnlineShop.Services.Auth.DataLayer.Models.Data;

namespace OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default);

        Task<ApplicationUser?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default);

        Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken = default);

        Task<bool> RoleExistsAsync(string roleName, CancellationToken cancellationToken = default);

        Task CreateRoleAsync(string roleName, CancellationToken cancellationToken = default);

        Task AddToRoleAsync(ApplicationUser user, string roleName);
    }
}
