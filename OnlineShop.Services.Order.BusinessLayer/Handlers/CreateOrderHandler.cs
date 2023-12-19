using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Models;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseDto<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderModel>(request.OrderCreateDto);

            await _orderRepository.CreateOrderAsync(order);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully created.",
                Result = _mapper.Map<OrderDto>(order)
            };
        }
    }
}
