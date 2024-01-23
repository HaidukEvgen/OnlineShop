namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public class InvalidBasketException : Exception
    {
        public InvalidBasketException(string id) : base(ExceptionMessages.InvalidBasket) { }
    }
}
