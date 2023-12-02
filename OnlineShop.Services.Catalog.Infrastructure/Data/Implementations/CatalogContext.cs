using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Infrastructure.Data.Interfaces;

namespace OnlineShop.Services.Catalog.Infrastructure.Data.Implementations
{
    public class CatalogContext : ICatalogContext
    {
        private readonly CatalogDbOptions _catalogDbSettings;

        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IOptions<CatalogDbOptions> catalogDbSettings)
        {
            _catalogDbSettings = catalogDbSettings.Value;
            var client = new MongoClient(_catalogDbSettings.ConnectionString);
            var database = client.GetDatabase(_catalogDbSettings.DatabaseName);
            Products = database.GetCollection<Product>(_catalogDbSettings.CollectionName);
        }

    }
}
