using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.BusinessLayer.Queries
{
    public class GetOrdersByStatusQuery : IRequest<ResponseDto<IEnumerable<OrderDto>>>
    {
        public OrderStatus Status { get; set; }
    }
}
