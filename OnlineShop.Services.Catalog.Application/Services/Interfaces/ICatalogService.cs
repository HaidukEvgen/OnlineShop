using OnlineShop.Services.Catalog.Application.Models.Dto;

namespace OnlineShop.Services.Catalog.Application.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ResponseDto<ProductDto>> GetProductAsync(string id);
        Task<ResponseDto<string>> AddProductAsync(NewProductDto productDto);
        Task<ResponseDto<object>> UpdateProductAsync(string id, NewProductDto productDto);
        Task<ResponseDto<object>> DeleteProductAsync(string id);
    }
}