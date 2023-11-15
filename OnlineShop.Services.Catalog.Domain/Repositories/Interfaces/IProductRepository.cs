using OnlineShop.Services.Catalog.Domain.Models.Data;

namespace OnlineShop.Services.Catalog.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(string id);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<string> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}