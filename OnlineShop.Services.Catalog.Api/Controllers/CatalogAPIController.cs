using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;

namespace OnlineShop.Services.Catalog.Api.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogAPIController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogAPIController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _catalogService.GetAllProductsAsync();

            return Ok(response);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _catalogService.GetProductAsync(id);

            return Ok(response);
        }

        [HttpGet("category/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string name)
        {
            var products = await _catalogService.GetProductsByCategoryAsync(name);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductDto productDto)
        {
            var response = await _catalogService.AddProductAsync(productDto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            var response = await _catalogService.UpdateProductAsync(productDto);

            return Ok(response);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _catalogService.DeleteProductAsync(id);

            return Ok(response);
        }
    }
}
