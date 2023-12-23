using Grpc.Core;
using OnlineShop.Services.Catalog.Application.Protos;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using AutoMapper;

namespace OnlineShop.Services.Catalog.Application.Services.gRPC
{
    public class CatalogGrpcService : CatalogService.CatalogServiceBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public CatalogGrpcService(ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
        }

        public override async Task<AreValidBasketItemsResponse> AreValidBasketItems(AreValidBasketItemsRequest request, ServerCallContext context)
        {
            var products = _mapper.Map<IEnumerable<GrpcProductDto>>(request.Items);

            bool areValid = await _catalogService.AreProductsValid(products);

            return new AreValidBasketItemsResponse { IsValid = areValid };
        }
    }
}
