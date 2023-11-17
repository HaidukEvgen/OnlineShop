using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;

namespace OnlineShop.Services.Catalog.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
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
        public async Task<IActionResult> GetProductAsync(string id)
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
        public async Task<IActionResult> UpdateProductAsync(string id, [FromBody] NewProductDto productDto)
        {
            var response = await _catalogService.UpdateProductAsync(id, productDto);

            return Ok(response);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            var response = await _catalogService.DeleteProductAsync(id);

            return Ok(response);
        }
    }
}
