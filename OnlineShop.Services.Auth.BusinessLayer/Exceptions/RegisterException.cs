namespace OnlineShop.Services.Auth.BusinessLayer.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() { }
        public RegisterException(string message) : base(message) { }
    }
}
