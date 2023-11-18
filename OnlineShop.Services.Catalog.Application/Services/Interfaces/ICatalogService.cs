using OnlineShop.Services.Catalog.Application.Models.Dto;

namespace OnlineShop.Services.Catalog.Application.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<ResponseDto<ProductDto>> GetProductAsync(string id, CancellationToken cancellationToken);
        Task<ResponseDto<string>> AddProductAsync(NewProductDto productDto, CancellationToken cancellationToken);
        Task<ResponseDto<object>> UpdateProductAsync(string id, NewProductDto productDto, CancellationToken cancellationToken);
        Task<ResponseDto<object>> DeleteProductAsync(string id, CancellationToken cancellationToken);
    }
}