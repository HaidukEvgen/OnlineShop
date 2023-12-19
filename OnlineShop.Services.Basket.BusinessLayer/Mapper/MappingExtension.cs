using AutoMapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.DataLayer.Models.Data;
using OnlineShop.Shared.MassTransit.Messages;

namespace OnlineShop.Services.Basket.BusinessLayer.Mapper
{
    public static class MappingExtensions
    {
        public static OrderCreatedEvent MapToOrderCreatedMessage(this IMapper mapper,
            ShoppingCart shoppingCart, OrderDetailsDto orderDetailsDto, string userId)
        {
            var orderCreated = mapper.Map<OrderCreatedEvent>(shoppingCart);
            mapper.Map(orderDetailsDto, orderCreated);
            orderCreated.UserId = userId;

            return orderCreated;
        }
    }
}
