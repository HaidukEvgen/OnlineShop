namespace OnlineShop.Services.AuthService.BusinessLayer.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() { }
        public RegisterException(string message) : base(message) { }
    }
}
