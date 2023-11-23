namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public decimal TotalPrice { get; set; }
    }
}
