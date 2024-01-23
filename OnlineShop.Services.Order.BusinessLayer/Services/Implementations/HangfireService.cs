using Hangfire;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Implementations
{
    public class HangfireService : IHangfireService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmailService _emailService;

        public HangfireService(IBackgroundJobClient backgroundJobClient, IEmailService emailService)
        {
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
        }

        public void EnqueueEmailSendingJob(OrderModel order, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() =>
                _emailService.SendEmailAsync(
                    order.Email,
                    EmailMessages.OrderCreatedEmailSubject,
                    EmailMessages.OrderCreatedEmailBody(order),
                    cancellationToken)
            );
        }

        public void ScheduleEmailSendingJob(OrderModel order, CancellationToken cancellationToken)
        {
            var jobDelay = CalculateJobDelayTime(order.EstimatedDeliveryDate);

            _backgroundJobClient.Schedule(() =>
                _emailService.SendEmailAsync(
                    order.Email,
                    EmailMessages.OrderDeliveryEmailSubject,
                    EmailMessages.OrderDeliveryEmailBody(order),
                    cancellationToken
                    ),
                jobDelay
            );
        }

        private TimeSpan CalculateJobDelayTime(DateOnly estimatedDeliveryDate)
        {
            var tomorrow = DateTime.Now.Date.AddDays(1);
            var estimatedDeliveryDateTime = new DateTime(estimatedDeliveryDate.Year, estimatedDeliveryDate.Month, estimatedDeliveryDate.Day);
            var fireTime = estimatedDeliveryDateTime.AddDays(-1);

            if (estimatedDeliveryDateTime < tomorrow.Date || estimatedDeliveryDateTime.Date <= DateTime.Now.Date)
            {
                fireTime = DateTime.Now;
            }

            return fireTime - DateTime.Now;
        }
    }
}
