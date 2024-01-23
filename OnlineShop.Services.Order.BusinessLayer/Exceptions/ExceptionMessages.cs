namespace OnlineShop.Services.Order.BusinessLayer.Exceptions
{
    public static class ExceptionMessages
    {
        public static string OrderNotFound(int id)
        {
            return $"Order with id {id} was not found in database";
        }
    }
}
