using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Queries
{
    public class GetOrdersByUserQuery : IRequest<ResponseDto<IEnumerable<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
