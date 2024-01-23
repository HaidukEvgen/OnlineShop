using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Queries
{
    public class GetOrderByIdQuery : IRequest<ResponseDto<OrderDto>>
    {
        public int Id { get; set; }
    }
}
