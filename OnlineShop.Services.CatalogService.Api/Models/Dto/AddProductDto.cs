namespace OnlineShop.Services.CatalogAPI.Models.Dto
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Dictionary<string, string> OptionalFields { get; set; }
    }
}
