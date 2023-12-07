using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class GetOrdersByUserHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersByUserQuery, ResponseDto<IEnumerable<OrderDto>>>
    {
        public async Task<ResponseDto<IEnumerable<OrderDto>>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUserAsync(request.UserId);

            return new ResponseDto<IEnumerable<OrderDto>>
            {
                Result = mapper.Map<IEnumerable<OrderDto>>(orders)
            };
        }
    }
}
