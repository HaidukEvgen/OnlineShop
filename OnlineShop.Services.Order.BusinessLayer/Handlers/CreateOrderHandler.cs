using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Order.DataLayer.Models;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Order.BusinessLayer.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseDto<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmailService _emailService;

        public CreateOrderHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IBackgroundJobClient backgroundJobClient,
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
        }

        public async Task<ResponseDto<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderModel>(request.OrderCreateDto);

            await _orderRepository.CreateOrderAsync(order, cancellationToken);

            _backgroundJobClient.Enqueue(() => _emailService.SendEmailAsync(order.Email, "Order created", $"Dear customer, your order with total price of ${order.Total} has been successfully created. The estimated delivery date is {order.EstimatedDeliveryDate.ToShortDateString()}. We will send you an email before the order will be delivered."));

            var jobDelay = CalculateJobDelayTime(order.EstimatedDeliveryDate);
            _backgroundJobClient.Schedule(() => _emailService.SendEmailAsync(order.Email, "Order created", $"Dear customer, we remind you that you order wit total price of ${order.Total} is going to be delivered on {order.EstimatedDeliveryDate.ToShortDateString()}"), jobDelay);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully created.",
                Result = _mapper.Map<OrderDto>(order)
            };
        }

        private TimeSpan CalculateJobDelayTime(DateOnly estimatedDeliveryDate)
        {
            var tomorrow = DateTime.Now.Date.AddDays(1);
            var estimatedDeliveryDateTime = new DateTime(estimatedDeliveryDate.Year, estimatedDeliveryDate.Month, estimatedDeliveryDate.Day); 
            var fireTime = estimatedDeliveryDateTime.AddDays(-1);

            if (estimatedDeliveryDateTime < tomorrow.Date || estimatedDeliveryDateTime.Date <= DateTime.Now.Date)
            {
                fireTime = DateTime.Now;
            }

            return fireTime - DateTime.Now;
        }
    }
}
