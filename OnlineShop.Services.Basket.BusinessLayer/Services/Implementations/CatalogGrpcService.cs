using AutoMapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Protos;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Implementations
{
    public class CatalogGrpcService : ICatalogGrpcService
    {
        private readonly CatalogService.CatalogServiceClient _client;
        private readonly IMapper _mapper;

        public CatalogGrpcService(IMapper mapper, CatalogService.CatalogServiceClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        public async Task<bool> AreValidBasketItemsAsync(IEnumerable<GrpcProductDto> grpcProductDtos)
        {
            var request = _mapper.Map<AreValidBasketItemsRequest>(grpcProductDtos);

            var response = await _client.AreValidBasketItemsAsync(request);

            return response.IsValid;
        }
    }
}