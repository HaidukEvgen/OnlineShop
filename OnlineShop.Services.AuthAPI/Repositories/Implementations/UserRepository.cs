using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.AuthAPI.AppData;
using OnlineShop.Services.AuthAPI.Models.Data;
using OnlineShop.Services.AuthAPI.Repositories.Interfaces;

namespace OnlineShop.Services.AuthAPI.Repositories.Implementations
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