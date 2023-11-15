using MongoDB.Driver;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;
using OnlineShop.Services.Catalog.Infrastructure.Data.Interfaces;

namespace OnlineShop.Services.Catalog.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _catalogContext
                     .Products
                     .Find(p => true)
                     .ToListAsync();

        public async Task<Product> GetAsync(string id)
            => await _catalogContext
                    .Products
                    .Find(product => product.Id == id)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Category, category);

            return await _catalogContext
                        .Products
                        .Find(filter)
                        .ToListAsync();
        }

        public async Task<string> AddAsync(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);

            return product.Id;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var updateResult = await _catalogContext
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _catalogContext
                                            .Products
                                            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
