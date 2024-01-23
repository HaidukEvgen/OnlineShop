using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class GetOrdersByStatusHandler : IRequestHandler<GetOrdersByStatusQuery, ResponseDto<IEnumerable<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByStatusHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<OrderDto>>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(request.Status, cancellationToken);

            return new ResponseDto<IEnumerable<OrderDto>>
            {
                Result = _mapper.Map<IEnumerable<OrderDto>>(orders)
            };
        }
    }
}
