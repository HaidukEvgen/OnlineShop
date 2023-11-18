using MongoDB.Driver;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;
using OnlineShop.Services.Catalog.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

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
        {
            return await _catalogContext
                        .Products
                        .Find(p => true)
                        .ToListAsync();
        }

        public async Task<Product> GetAsync(string id)
        {
            return await _catalogContext
                        .Products
                        .Find(product => product.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            var filter = BuildEqualityFilter(product => product.Category, category);

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

        public async Task UpdateAsync(Product product)
        {
            var filter = BuildEqualityFilter(p => p.Id, product.Id);

            var updateResult = await _catalogContext
                                    .Products
                                    .ReplaceOneAsync(filter, product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = BuildEqualityFilter(p => p.Id, id);

            var deleteResult = await _catalogContext
                                            .Products
                                            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        private FilterDefinition<Product> BuildEqualityFilter(Expression<Func<Product, string>> field, string value)
        {
            return Builders<Product>.Filter.Eq(new ExpressionFieldDefinition<Product, string>(field), value);
        }
    }
}
