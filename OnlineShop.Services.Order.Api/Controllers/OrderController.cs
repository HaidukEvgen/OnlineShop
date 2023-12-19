using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Order.BusinessLayer.Commands;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using OnlineShop.Services.Order.BusinessLayer.Queries;
using OnlineShop.Services.Order.DataLayer.Models;

namespace OnlineShop.Services.Order.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var query = new GetOrdersQuery();
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var query = new GetOrderByIdQuery { Id = id };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserAsync(string userId)
        {
            var query = new GetOrdersByUserQuery { UserId = userId };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatusAsync(OrderStatus status)
        {
            var query = new GetOrdersByStatusQuery { Status = status };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderCreateDto orderCreateDto)
        {
            var command = new CreateOrderCommand { OrderCreateDto = orderCreateDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var command = new UpdateOrderCommand { Id = id, OrderUpdateDto = orderUpdateDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
