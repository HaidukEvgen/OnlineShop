namespace OnlineShop.Shared.MassTransit.Messages
{
    public class OrderCreatedEvent
    {
        public string UserId { get; set; }
        public IEnumerable<string> ProductIds { get; set; }
        public decimal Total { get; set; }
        public string DeliveryAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Comment { get; set; }
    }
}
