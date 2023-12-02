using AutoMapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.DataLayer.Models.Data;

namespace OnlineShop.Services.Basket.BusinessLayer.Mapper
{
    public static class MappingExtensions
    {
        public static OrderCreateDto MapToOrderCreateDto(this IMapper mapper,
            ShoppingCart shoppingCart, OrderDetailsDto orderDetailsDto, string userId)
        {
            var orderCreateDto = mapper.Map<OrderCreateDto>(shoppingCart);
            mapper.Map(orderDetailsDto, orderCreateDto);
            orderCreateDto.UserId = userId;
            return orderCreateDto;
        }
    }
}
