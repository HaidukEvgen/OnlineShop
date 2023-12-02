namespace OnlineShop.Services.Catalog.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string id) : base($"Product with id = {id} was not found in database") { }
    }
}
