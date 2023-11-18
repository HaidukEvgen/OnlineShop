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

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _catalogContext
                        .Products
                        .Find(p => true)
                        .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetAsync(string id, CancellationToken cancellationToken)
        {
            return await _catalogContext
                        .Products
                        .Find(product => product.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _catalogContext.Products.InsertOneAsync(product, options: null, cancellationToken );

            return product.Id;
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var filter = BuildEqualityFilter(p => p.Id, product.Id);

            var updateResult = await _catalogContext
                                    .Products
                                    .ReplaceOneAsync(filter, product, cancellationToken: cancellationToken);
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var filter = BuildEqualityFilter(p => p.Id, id);

            var deleteResult = await _catalogContext
                                            .Products
                                            .DeleteOneAsync(filter, cancellationToken: cancellationToken);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        private FilterDefinition<Product> BuildEqualityFilter(Expression<Func<Product, string>> field, string value)
        {
            return Builders<Product>.Filter.Eq(new ExpressionFieldDefinition<Product, string>(field), value);
        }
    }
}
