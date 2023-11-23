namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class OrderCreateDto
    {
        public string UserId { get; set; }

        public IEnumerable<string> ProductIds { get; set; }

        public decimal Total { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string? Comment { get; set; }

    }
}
