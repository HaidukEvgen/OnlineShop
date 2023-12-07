using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Commands
{
    public class UpdateOrderCommand : IRequest<ResponseDto<OrderDto>>
    {
        public int Id { get; set; }
        public OrderUpdateDto OrderUpdateDto { get; set; }
    }
}
