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
        private readonly IHangfireService _hangfireService;
        private readonly IEmailService _emailService;

        public CreateOrderHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IHangfireService hangfireService,
            IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _hangfireService = hangfireService;
            _emailService = emailService;
        }

        public async Task<ResponseDto<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderModel>(request.OrderCreateDto);

            await _orderRepository.CreateOrderAsync(order, cancellationToken);

            _hangfireService.EnqueueEmailSendingJob(order);
            _hangfireService.ScheduleEmailSendingJob(order);

            return new ResponseDto<OrderDto>
            {
                Message = "Successfully created.",
                Result = _mapper.Map<OrderDto>(order)
            };
        }
    }
}
