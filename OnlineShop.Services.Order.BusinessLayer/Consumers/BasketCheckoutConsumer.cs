using AutoMapper;
using MassTransit;
using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Shared.MassTransit.Messages;

namespace OnlineShop.Services.Order.BusinessLayer.Consumers
{
    public class BasketCheckoutConsumer(IMediator mediator, IMapper mapper) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderCreateDto = mapper.Map<OrderCreateDto>(context.Message);
            await mediator.Send(new CreateOrderCommand() { OrderCreateDto = orderCreateDto });
        }
    }
}
