namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
    }
}