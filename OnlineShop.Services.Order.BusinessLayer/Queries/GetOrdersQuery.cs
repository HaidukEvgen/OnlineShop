using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Queries
{
    public class GetOrdersQuery : IRequest<ResponseDto<IEnumerable<OrderDto>>> { }
}
