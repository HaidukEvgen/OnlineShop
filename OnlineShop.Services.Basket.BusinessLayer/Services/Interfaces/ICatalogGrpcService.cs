using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces
{
    public interface ICatalogGrpcService
    {
        Task<bool> AreValidBasketItems(IEnumerable<GrpcProductDto> grpcProductDtos);
    }
}
