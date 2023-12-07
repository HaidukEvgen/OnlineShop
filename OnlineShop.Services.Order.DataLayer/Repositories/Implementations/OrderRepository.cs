using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.DataLayer.AppData;
using OnlineShop.Services.Order.DataLayer.Models;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.DataLayer.Repositories.Implementations
{
    public class OrderRepository(OrderContext context) : IOrderRepository
    {

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync() 
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserAsync(string userId)
        {
            return await context.Orders.Where(order => order.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await context.Orders.Where(order => order.Status == status).ToListAsync();
        }

        public async Task CreateOrderAsync(OrderModel order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(OrderModel order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(OrderModel order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }
}
