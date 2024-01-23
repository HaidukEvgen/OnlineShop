using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetOrdersAsync(CancellationToken cancellationToken);

        Task<OrderModel?> GetOrderByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<OrderModel>> GetOrdersByUserAsync(string userId, CancellationToken cancellationToken);

        Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken);

        Task CreateOrderAsync(OrderModel order, CancellationToken cancellationToken);

        Task UpdateOrderAsync(OrderModel order, CancellationToken cancellationToken);

        Task DeleteOrderAsync(OrderModel order, CancellationToken cancellationToken);
    }
}
