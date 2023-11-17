namespace OnlineShop.Services.Catalog.Application.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException(string message) : base(message) { }
    }
}
