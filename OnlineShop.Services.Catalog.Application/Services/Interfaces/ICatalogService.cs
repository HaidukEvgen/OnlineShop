using OnlineShop.Services.Catalog.Application.Models.Dto;

namespace OnlineShop.Services.Catalog.Application.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetProductAsync(string id);
        Task<ResponseDto> GetProductsByCategoryAsync(string category);
        Task<ResponseDto> AddProductAsync(NewProductDto productDto);
        Task<ResponseDto> UpdateProductAsync(string id, NewProductDto productDto);
        Task<ResponseDto> DeleteProductAsync(string id);
    }
}