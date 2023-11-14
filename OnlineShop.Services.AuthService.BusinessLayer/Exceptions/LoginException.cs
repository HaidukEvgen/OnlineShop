namespace OnlineShop.Services.AuthService.BusinessLayer.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() { }
        public LoginException(string message) : base(message) { }
    }
}
