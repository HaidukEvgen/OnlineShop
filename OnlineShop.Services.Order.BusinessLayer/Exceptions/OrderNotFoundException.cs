namespace OnlineShop.Services.Order.BusinessLayer.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id) : base(ExceptionMessages.OrderNotFound(id)) { }
    }
}
