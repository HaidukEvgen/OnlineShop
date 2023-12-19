using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ResponseDto>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ResponseDto> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            await _orderRepository.DeleteOrderAsync(order, cancellationToken);

            return new ResponseDto
            {
                Message = "Successfully deleted."
            };
        }
    }
}
