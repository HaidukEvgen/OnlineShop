using MongoDB.Driver;
using OnlineShop.Services.Catalog.Domain.Models.Data;

namespace OnlineShop.Services.Catalog.Infrastructure.Data.Interfaces
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
