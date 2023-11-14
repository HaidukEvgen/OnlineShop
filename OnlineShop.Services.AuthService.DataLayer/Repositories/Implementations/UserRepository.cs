using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.AuthService.DataLayer.AppData;
using OnlineShop.Services.AuthService.DataLayer.Models.Data;
using OnlineShop.Services.AuthService.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.AuthService.DataLayer.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {   
        private readonly AppDbContext _context;
        private readonly DbSet<ApplicationUser> _dbSet;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _dbSet = _context.Set<ApplicationUser>();
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password) =>
            await _userManager.CreateAsync(user, password);


        public async Task<ApplicationUser?> GetByNameAsync(string name) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Name == name);

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password) =>
            await _userManager.CheckPasswordAsync(user, password);

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user) =>
            await _userManager.GetRolesAsync(user);

        public async Task<bool> RoleExistsAsync(string roleName) =>
            await _roleManager.RoleExistsAsync(roleName);

        public async Task CreateRoleAsync(string roleName) =>
            await _roleManager.CreateAsync(new IdentityRole(roleName));

        public async Task AddToRoleAsync(ApplicationUser user, string roleName) =>
            await _userManager.AddToRoleAsync(user, roleName);
    }
}