using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ResponseDto<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id);

            if (order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            _mapper.Map(request.OrderUpdateDto, order);

            await _orderRepository.UpdateOrderAsync(order);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully updated.",
                Result = _mapper.Map<OrderDto>(order)
            };
        }
    }
}
