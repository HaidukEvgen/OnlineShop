using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasketAsync(string userId, CancellationToken cancellationToken = default);
        Task<ResponseDto<BasketDto>> UpdateBasketAsync(string userId, UpdateBasketDto basketDto, CancellationToken cancellationToken = default);
        Task<ResponseDto> DeleteBasketAsync(string userId, CancellationToken cancellationToken = default);
        Task<ResponseDto> CreateOrderAsync(string userId, OrderDetailsDto orderCreateDto, CancellationToken cancellationToken = default);
    }
}
