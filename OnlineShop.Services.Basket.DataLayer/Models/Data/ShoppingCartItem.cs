namespace OnlineShop.Services.Basket.DataLayer.Models.Data
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }   
    }
}
