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
    public class OrderController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var response = await mediator.Send(new GetOrdersQuery());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var response = await mediator.Send(new GetOrderByIdQuery() { Id = id });

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserAsync(string userId)
        {
            var response = await mediator.Send(new GetOrdersByUserQuery() { UserId = userId });

            return Ok(response);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatusAsync(OrderStatus status)
        {
            var response = await mediator.Send(new GetOrdersByStatusQuery() { Status = status });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderCreateDto orderCreateDto)
        {
            var response = await mediator.Send(new CreateOrderCommand() { OrderCreateDto = orderCreateDto });

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var response = await mediator.Send(new UpdateOrderCommand() { Id = id, OrderUpdateDto = orderUpdateDto });

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var response = await mediator.Send(new DeleteOrderCommand() { Id = id });

            return Ok(response);
        }
    }
}
