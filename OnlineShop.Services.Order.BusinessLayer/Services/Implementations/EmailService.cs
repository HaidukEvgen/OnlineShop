using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(EmailSetup.MailBoxName, EmailSetup.MailBoxAddress));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(EmailSetup.EmailHost, EmailSetup.EmailHostPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(EmailSetup.MailBoxAddress, EmailSetup.MailBoxPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
