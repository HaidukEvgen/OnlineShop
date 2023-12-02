namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class UpdateBasketDto
    {
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
