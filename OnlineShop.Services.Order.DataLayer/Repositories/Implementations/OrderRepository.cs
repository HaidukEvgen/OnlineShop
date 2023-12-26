using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.DataLayer.AppData;
using OnlineShop.Services.Order.DataLayer.Models;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.DataLayer.Repositories.Implementations
{
    public class OrderRepository(OrderContext context) : IOrderRepository
    {

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(CancellationToken cancellationToken = default)
        {
            return await context.Orders.ToListAsync(cancellationToken);
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Orders.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await context.Orders.Where(order => order.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default)
        {
            return await context.Orders.Where(order => order.Status == status).ToListAsync(cancellationToken);
        }

        public async Task CreateOrderAsync(OrderModel order, CancellationToken cancellationToken = default)
        {
            await context.Orders.AddAsync(order, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateOrderAsync(OrderModel order, CancellationToken cancellationToken = default)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrderAsync(OrderModel order, CancellationToken cancellationToken = default)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
