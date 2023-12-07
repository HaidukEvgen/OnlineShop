using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class GetOrdersByStatusHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersByStatusQuery, ResponseDto<IEnumerable<OrderDto>>>
    {
        public async Task<ResponseDto<IEnumerable<OrderDto>>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByStatusAsync(request.Status);

            return new ResponseDto<IEnumerable<OrderDto>>
            {
                Result = mapper.Map<IEnumerable<OrderDto>>(orders)
            };
        }
    }
}
