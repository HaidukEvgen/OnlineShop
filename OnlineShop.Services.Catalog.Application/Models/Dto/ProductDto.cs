namespace OnlineShop.Services.Catalog.Application.Models.Dto
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Dictionary<string, string> OptionalFields { get; set; }
    }
}
