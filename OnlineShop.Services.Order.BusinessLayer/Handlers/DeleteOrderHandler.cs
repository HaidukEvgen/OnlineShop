using AutoMapper;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class DeleteOrderHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand, ResponseDto<object>>
    {
        public async Task<ResponseDto<object>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetOrderByIdAsync(request.Id);

            if (order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            await orderRepository.DeleteOrderAsync(order);

            return new ResponseDto<object>
            {
                Message = "Successfully deleted."
            };
        }
    }
}
