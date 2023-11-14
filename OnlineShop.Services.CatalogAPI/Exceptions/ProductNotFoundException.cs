using OnlineShop.Services.CatalogAPI.Models.Data;

namespace OnlineShop.Services.CatalogAPI.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string id) : base($"Product with id = {id} was not found in database") { }
    }
}
