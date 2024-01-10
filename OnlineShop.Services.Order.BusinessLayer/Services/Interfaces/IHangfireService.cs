using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Interfaces
{
    public interface IHangfireService
    {
        void EnqueueEmailSendingJob(OrderModel order, CancellationToken cancellationToken = default);
        void ScheduleEmailSendingJob(OrderModel order, CancellationToken cancellationToken = default);
    }
}
