using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Models.Dto
{
    public class OrderUpdateDto
    {
        public OrderStatus Status { get; set; }

        public DateOnly? ActualDeliveryDate { get; set; }

    }
}
