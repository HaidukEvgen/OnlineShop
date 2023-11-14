using OnlineShop.Services.CatalogAPI.Models.Dto;

namespace OnlineShop.Services.CatalogAPI.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetProductAsync(string id);
        Task<ResponseDto> GetProductsByCategoryAsync(string category);
        Task<ResponseDto> AddProductAsync(AddProductDto productDto);
        Task<ResponseDto> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto> DeleteProductAsync(string id);
    }
}