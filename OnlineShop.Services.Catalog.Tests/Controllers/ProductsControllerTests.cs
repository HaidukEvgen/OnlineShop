using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Services.Catalog.Api.Controllers;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using OnlineShop.Services.Catalog.Tests.FakeData;
using OnlineShop.Services.Catalog.Tests.Helpers;

namespace OnlineShop.Services.Catalog.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<ICatalogService> _mockCatalogService;
        private readonly CatalogFakeDataGenerator _fakeData;
        private readonly ProductsControllerHelper _helper;
        private readonly CancellationToken _cancellationToken;

        public ProductsControllerTests()
        {
            _mockCatalogService = new Mock<ICatalogService>();
            _controller = new ProductsController(_mockCatalogService.Object);
            _fakeData = new CatalogFakeDataGenerator();
            _helper = new ProductsControllerHelper(_mockCatalogService);
            _cancellationToken = CancellationToken.None;
        }

        [Fact]
        public async Task GetProdcuts_ShouldReturnOkWithProducts()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                _fakeData.GenerateFakeProductDto(),
                _fakeData.GenerateFakeProductDto(),
                _fakeData.GenerateFakeProductDto(),
            };

            _helper.SetupGetAllAsync(products);

            // Act
            var result = await _controller.GetProductsAsync(_cancellationToken);

            // Assert
            var okResult = result
                .Should().BeOfType<OkObjectResult>().Subject;

            okResult.Value
                .Should().BeOfType<ResponseDto<IEnumerable<ProductDto>>>();
        }

        [Fact]
        public async Task GetProduct_WhenItIsFound_ShouldReturnOkWithProduct()
        {
            // Arrange
            var product = _fakeData.GenerateFakeProductDto();

            _helper.SetupGetByIdAsync(product);

            // Act
            var result = await _controller.GetProductAsync(product.Id, _cancellationToken);

            // Assert
            var okResult = result
                .Should().BeOfType<OkObjectResult>().Subject;

            okResult.Value
                .Should().BeOfType<ResponseDto<ProductDto>>();
        }

        [Fact]
        public async Task AddProduct_WhenModelIsValid_ShouldReturnOkWithId()
        {
            // Arrange
            var product = _fakeData.GenerateFakeNewProductDto();

            _helper.SetupAddAsync(product);

            // Act
            var result = await _controller.AddProductAsync(product, _cancellationToken);

            // Assert
            var okResult = result
                .Should().BeOfType<OkObjectResult>().Subject;

            okResult.Value
                .Should().BeOfType<ResponseDto<string>>();
        }

        [Fact]
        public async Task UpdateProduct_WhenModelIsFound_ShouldReturnOk()
        {
            // Arrange
            var product = _fakeData.GenerateFakeNewProductDto();
            var id = _fakeData.GenerateFakeProductId();

            _helper.SetupUpdateAsync(id, product);

            // Act
            var result = await _controller.UpdateProductAsync(id, product, _cancellationToken);

            // Assert
            result
                .Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async Task DeleteProduct_WhenModelIsFound_ShouldReturnOk()
        {
            // Arrange
            var id = _fakeData.GenerateFakeProductId();

            _helper.SetupDeleteAsync(id);

            // Act
            var result = await _controller.DeleteProductAsync(id, _cancellationToken);

            // Assert
            result
                .Should().BeOfType<OkObjectResult>();
        }
    }
}
