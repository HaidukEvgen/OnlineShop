using AutoMapper;
using MassTransit;
using OnlineShop.Services.Basket.BusinessLayer.Exceptions;
using OnlineShop.Services.Basket.BusinessLayer.Mapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Basket.DataLayer.Models.Data;
using OnlineShop.Services.Basket.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Basket.BusinessLayer.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICatalogGrpcService _catalogGrpcService;

        public BasketService(
            IBasketRepository basketRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            ICatalogGrpcService catalogGrpcService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _catalogGrpcService = catalogGrpcService;
        }

        public async Task<ResponseDto<BasketDto>> GetBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(userId, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(userId);
            }

            var basketDto = _mapper.Map<BasketDto>(basket);
            basketDto.UserId = userId;

            return new ResponseDto<BasketDto> { Result = basketDto };
        }

        public async Task<ResponseDto<BasketDto>> UpdateBasketAsync(string userId, UpdateBasketDto basketDto, CancellationToken cancellationToken = default)
        {

            var updatedBasket = _mapper.Map<ShoppingCart>(basketDto);

            updatedBasket = await _basketRepository.UpdateBasketAsync(userId, updatedBasket, cancellationToken);

            var result = _mapper.Map<BasketDto>(updatedBasket);
            result.UserId = userId;

            return new ResponseDto<BasketDto> { Result = result, Message = "Basket updated successfully" };
        }

        public async Task<ResponseDto> DeleteBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            await _basketRepository.DeleteBasketAsync(userId, cancellationToken);

            return new ResponseDto { Message = "Basket deleted successfully" };
        }

        public async Task<ResponseDto> CreateOrderAsync(string userId, OrderDetailsDto orderDetailsDto, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(userId, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(userId);
            }

            var grpcProductDtos = _mapper.Map<IEnumerable<GrpcProductDto>>(basket.Items);

            var areValid = await _catalogGrpcService.AreValidBasketItems(grpcProductDtos);

            if (!areValid)
            {
                throw new InvalidBasketException(userId);
            }

            var checkoutMessage = _mapper.MapToOrderCreatedMessage(basket, orderDetailsDto, userId);

            await _publishEndpoint.Publish(checkoutMessage, cancellationToken);

            await DeleteBasketAsync(userId, cancellationToken);

            return new ResponseDto<object> { Message = "Order created successfully." };
        }
    }
}
