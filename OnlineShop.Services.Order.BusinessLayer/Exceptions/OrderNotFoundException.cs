namespace OnlineShop.Services.Order.BusinessLayer.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id) : base($"Order with id {id} was not found in database") { }
    }
}
