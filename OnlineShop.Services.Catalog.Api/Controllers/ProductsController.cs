using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;

namespace OnlineShop.Services.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public ProductsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(CancellationToken cancellationToken)
        {
            var response = await _catalogService.GetAllProductsAsync(cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var response = await _catalogService.GetProductAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] NewProductDto productDto, CancellationToken cancellationToken)
        {
            var response = await _catalogService.AddProductAsync(productDto, cancellationToken);

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] string id, [FromBody] NewProductDto productDto, CancellationToken cancellationToken)
        {
            var response = await _catalogService.UpdateProductAsync(id, productDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var response = await _catalogService.DeleteProductAsync(id, cancellationToken);

            return Ok(response);
        }
    }
}
