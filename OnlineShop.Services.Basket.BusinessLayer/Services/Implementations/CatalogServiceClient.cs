using OnlineShop.Services.Basket.BusinessLayer.Protos;
using Grpc.Net.Client;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;
using AutoMapper;

public class CatalogServiceClient : ICatalogServiceClient
{
    private readonly CatalogService.CatalogServiceClient _client;
    private readonly IMapper _mapper;

    public CatalogServiceClient(IMapper mapper)
    {
        _mapper = mapper;

        var channel = GrpcChannel.ForAddress("https://catalog_service:7010", new GrpcChannelOptions
        {
            HttpHandler = GetHttpClientHandler()
        });
        _client = new CatalogService.CatalogServiceClient(channel);
    }

    public async Task<bool> AreValidBasketItems(IEnumerable<GrpcProductDto> grpcProductDtos)
    {
        var request = _mapper.Map<AreValidBasketItemsRequest>(grpcProductDtos);

        var response = await _client.AreValidBasketItemsAsync(request);

        return response.IsValid;
    }

    private HttpClientHandler GetHttpClientHandler()
    {
        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        return httpHandler;
    }
}