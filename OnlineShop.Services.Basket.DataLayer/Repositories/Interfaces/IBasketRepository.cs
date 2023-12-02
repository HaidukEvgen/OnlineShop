using OnlineShop.Services.Basket.DataLayer.Models.Data;

namespace OnlineShop.Services.Basket.DataLayer.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetBasketAsync(string userId, CancellationToken cancellationToken = default);
        Task<ShoppingCart> UpdateBasketAsync(string userId, ShoppingCart basket, CancellationToken cancellationToken = default);
        Task DeleteBasketAsync(string userId, CancellationToken cancellationToken = default);
    }
}
