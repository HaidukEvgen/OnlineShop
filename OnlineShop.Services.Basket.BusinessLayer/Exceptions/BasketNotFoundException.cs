namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(string id) : base($"Basket of user with id = {id} was not found in database") { }
    }
}
