using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces
{
    public interface ICatalogServiceClient
    {
        Task<bool> AreValidBasketItems(IEnumerable<GrpcProductDto> grpcProductDtos);
    }
}
