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
        public async Task<IActionResult> GetProductsAsync()
        {
            var response = await _catalogService.GetAllProductsAsync();

            return Ok(response);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] string id)
        {
            var response = await _catalogService.GetProductAsync(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] NewProductDto productDto)
        {
            var response = await _catalogService.AddProductAsync(productDto);

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] string id, [FromBody] NewProductDto productDto)
        {
            var response = await _catalogService.UpdateProductAsync(id, productDto);

            return Ok(response);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] string id)
        {
            var response = await _catalogService.DeleteProductAsync(id);

            return Ok(response);
        }
    }
}
