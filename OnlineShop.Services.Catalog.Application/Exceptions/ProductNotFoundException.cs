namespace OnlineShop.Services.Catalog.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string id) : base(ExceptionMessages.ProductNotFound(id)) { }
    }
}
