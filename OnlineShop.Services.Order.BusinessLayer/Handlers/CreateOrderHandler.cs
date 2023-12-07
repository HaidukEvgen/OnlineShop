using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Models;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class CreateOrderHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<CreateOrderCommand, ResponseDto<OrderDto>>
    {
        public async Task<ResponseDto<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<OrderModel>(request.OrderCreateDto);

            await orderRepository.CreateOrderAsync(order);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully created.",
                Result = mapper.Map<OrderDto>(order)
            };
        }
    }
}
