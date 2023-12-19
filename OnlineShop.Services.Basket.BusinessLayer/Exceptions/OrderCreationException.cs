namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public class OrderCreationException : Exception
    {
        public OrderCreationException(string statusCode) : base(ExceptionMessages.OrderCreationFailed(statusCode)) { }
    }
}
