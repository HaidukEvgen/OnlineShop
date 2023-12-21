using AutoMapper;
using Grpc.Core;
using OnlineShop.Services.Catalog.Api.Protos;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;

namespace OnlineShop.Services.Catalog.Api.Services.gRPC
{
    public class CatalogGrpcService : CatalogService.CatalogServiceBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogGrpcService(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public override async Task<AreValidBasketItemsResponse> AreValidBasketItems(AreValidBasketItemsRequest request, ServerCallContext context)
        {
            var products = request.Items.Select(item => new GrpcProductDto
            {
                Price = (decimal)item.PriceInCents / 100,
                Id = item.ProductId
            }).ToList();

            bool areValid =  await _catalogService.AreProductsValid(products);

            return new AreValidBasketItemsResponse { IsValid = areValid };
        }
    }
}
