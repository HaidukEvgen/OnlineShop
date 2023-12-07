using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrdersAsync();

        Task<OrderModel?> GetOrderByIdAsync(int id);

        Task<IEnumerable<OrderModel>> GetOrdersByUserAsync(string userId);

        Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(OrderStatus status);

        Task CreateOrderAsync(OrderModel order);

        Task UpdateOrderAsync(OrderModel order);

        Task DeleteOrderAsync(OrderModel order);
    }
}
