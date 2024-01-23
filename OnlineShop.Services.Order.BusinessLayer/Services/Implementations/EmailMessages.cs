using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Services.Implementations
{
    public static class EmailMessages
    {
        public const string OrderCreatedEmailSubject = "Order created";
        public const string OrderDeliveryEmailSubject = "Order delivery";
        public static string OrderCreatedEmailBody(OrderModel order)
        {
            return $"Dear customer, your order with total price of ${order.Total} has been successfully created. The estimated delivery date is {order.EstimatedDeliveryDate.ToShortDateString()}. We will send you an email before the order will be delivered.";
        }

        public static string OrderDeliveryEmailBody(OrderModel order)
        {
            return $"Dear customer, we remind you that you order wit total price of ${order.Total} is going to be delivered on {order.EstimatedDeliveryDate.ToShortDateString()}";
        }
    }
}
