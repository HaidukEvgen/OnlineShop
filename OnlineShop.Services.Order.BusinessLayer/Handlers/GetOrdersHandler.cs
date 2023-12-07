using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class GetOrdersHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ResponseDto<IEnumerable<OrderDto>>>
    {
        public async Task<ResponseDto<IEnumerable<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersAsync();

            return new ResponseDto<IEnumerable<OrderDto>>
            {
                Result = mapper.Map<IEnumerable<OrderDto>>(orders)
            };
        }
    }
}
