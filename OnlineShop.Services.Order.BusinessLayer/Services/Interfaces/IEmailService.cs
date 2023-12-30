namespace OnlineShop.Services.Order.BusinessLayer.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
