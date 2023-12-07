using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<UpdateOrderCommand, ResponseDto<OrderDto>>
    {
        public async Task<ResponseDto<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetOrderByIdAsync(request.Id);

            if (order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            mapper.Map(request.OrderUpdateDto, order);

            await orderRepository.UpdateOrderAsync(order);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully updated.",
                Result = mapper.Map<OrderDto>(order)
            };
        }
    }
}
