namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(string id) : base(ExceptionMessages.BasketNotFound(id)) { }
    }
}
