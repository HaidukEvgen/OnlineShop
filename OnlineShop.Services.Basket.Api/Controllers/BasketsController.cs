using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;

namespace OnlineShop.Services.Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBasketAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            var response = await _basketService.GetBasketAsync(userId, cancellationToken);

            return Ok(response);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBasketAsync([FromRoute] string userId, [FromBody] UpdateBasketDto basketDto, CancellationToken cancellationToken)
        {
            var response = await _basketService.UpdateBasketAsync(userId, basketDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBasketAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            var response = await _basketService.DeleteBasketAsync(userId, cancellationToken);

            return Ok(response);
        }

        [HttpPost("{userId}/checkout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync([FromRoute] string userId, [FromBody] OrderDetailsDto orderDetailsDto, CancellationToken cancellationToken)
        {
            var response = await _basketService.CreateOrderAsync(userId, orderDetailsDto, cancellationToken);

            return Ok(response);
        }
    }
}
