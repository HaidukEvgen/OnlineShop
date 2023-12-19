namespace OnlineShop.Services.Order.DataLayer.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Products { get; set; }
        public string OrderNumber { get; set; }
        public decimal Total { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateOnly EstimatedDeliveryDate { get; set; } = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(2));
        public DateOnly? ActualDeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string? Comment { get; set; }
        public string GenerateOrderNumber()
        {
            return $"{Timestamp}-{UserId}";
        }
    }
}
