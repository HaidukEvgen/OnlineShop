using OnlineShop.Services.Catalog.Domain.Models.Data;

namespace OnlineShop.Services.Catalog.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetAsync(string id, CancellationToken cancellationToken);
        Task<string> AddAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}