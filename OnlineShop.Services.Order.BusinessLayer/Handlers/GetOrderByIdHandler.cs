using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class GetOrderByIdHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, ResponseDto<OrderDto>>
    {
        public async Task<ResponseDto<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetOrderByIdAsync(request.Id);

            if (order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            return new ResponseDto<OrderDto>
            {
                Result = mapper.Map<OrderDto>(order)
            };
        }
    }
}
