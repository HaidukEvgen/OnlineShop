using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.DataLayer.AppData
{
    public class OrderContext(DbContextOptions<OrderContext> option) : DbContext(option)
    {
        public DbSet<OrderModel> Orders { get; set; }
    }
}
