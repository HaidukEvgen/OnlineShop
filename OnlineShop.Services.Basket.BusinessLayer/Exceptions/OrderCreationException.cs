namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public class OrderCreationException : Exception
    {
        public OrderCreationException(string statusCode) : base($"Error creating order. Status code: {statusCode}") { }
    }
}
