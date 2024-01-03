using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using OnlineShop.Services.Order.BusinessLayer.Settings;
using Microsoft.Extensions.Options;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetupOptions _emailSetupOptions;

        public EmailService(IOptions<EmailSetupOptions> emailSetupOptions)
        {
            _emailSetupOptions = emailSetupOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string messageBody, CancellationToken cancellationToken = default)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSetupOptions.MailBoxName, _emailSetupOptions.MailBoxAddress));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSetupOptions.EmailHost, _emailSetupOptions.EmailHostPort, SecureSocketOptions.StartTls, cancellationToken);
                await client.AuthenticateAsync(_emailSetupOptions.MailBoxAddress, _emailSetupOptions.MailBoxPassword, cancellationToken);
                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
