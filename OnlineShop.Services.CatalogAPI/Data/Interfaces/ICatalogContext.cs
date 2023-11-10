using MongoDB.Driver;
using OnlineShop.Services.CatalogAPI.Models.Data;

namespace OnlineShop.Services.CatalogAPI.Data.Interfaces
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
