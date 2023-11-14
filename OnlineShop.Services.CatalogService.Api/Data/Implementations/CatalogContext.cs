using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Services.CatalogAPI.Data.Interfaces;
using OnlineShop.Services.CatalogAPI.Models.Data;

namespace OnlineShop.Services.CatalogAPI.Data.Implementations
{
    public class CatalogContext : ICatalogContext
    {
        private readonly CatalogDbOptions _catalogDbSettings;

        public CatalogContext(IOptions<CatalogDbOptions> catalogDbSettings)
        {
            _catalogDbSettings = catalogDbSettings.Value;
            var client = new MongoClient(_catalogDbSettings.ConnectionString);
            var database = client.GetDatabase(_catalogDbSettings.DatabaseName);
            Products = database.GetCollection<Product>(_catalogDbSettings.CollectionName);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
