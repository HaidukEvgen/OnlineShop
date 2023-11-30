using AutoMapper;
using Newtonsoft.Json;
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

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
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

        public async Task<ResponseDto<object>> DeleteBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            await _basketRepository.DeleteBasketAsync(userId, cancellationToken);

            return new ResponseDto<object> { Message = "Basket deleted successfully" };
        }

        public async Task<ResponseDto<object>> CreateOrderAsync(string userId, OrderDetailsDto orderDetailsDto, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(userId, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(userId);
            }

            var orderCreateDto = _mapper.MapToOrderCreateDto(basket, orderDetailsDto, userId);

            //TODO: Replace this temporary synchronous communication with RabbitMQ message bus
            var apiUrl = "https://localhost:7004/api/orders";

            using var httpClient = new HttpClient();
            
            var jsonContent = JsonConvert.SerializeObject(orderCreateDto);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, httpContent, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                await DeleteBasketAsync(userId, cancellationToken);

                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                return new ResponseDto<object> { Message = "Order created successfully.", Result = responseBody };
            }
            else
            {
                throw new OrderCreationException(response.StatusCode.ToString());
            }            
        }
    }
}
