using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Commands
{
    public class CreateOrderCommand : IRequest<ResponseDto<OrderDto>>
    {
        public OrderCreateDto OrderCreateDto { get; set; }
    }
}
