namespace OnlineShop.Services.AuthAPI.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() { }
        public LoginException(string message) : base(message) { }
    }
}
