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

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email) =>
            await _dbSet.FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
    }
}