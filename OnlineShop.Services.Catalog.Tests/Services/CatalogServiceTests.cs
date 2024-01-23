using AutoMapper;
using Moq;
using OnlineShop.Services.Catalog.Application.Exceptions;
using OnlineShop.Services.Catalog.Application.Services.Implementations;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;
using OnlineShop.Services.Catalog.Tests.FakeData;
using OnlineShop.Services.Catalog.Tests.Helpers;

namespace OnlineShop.Services.Catalog.Tests.Services
{
    public class CatalogServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly CatalogFakeDataGenerator _fakeData;
        private readonly CatalogServiceHelper _helper;
        private readonly CancellationToken _cancellationToken;
        private readonly ICatalogService _catalogService;

        public CatalogServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _fakeData = new CatalogFakeDataGenerator();
            _helper = new CatalogServiceHelper(_mockMapper, _mockProductRepository);
            _cancellationToken = CancellationToken.None;
            _catalogService = new CatalogService(_mockProductRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenProductsAreFound_ShouldReturnListOfProductsDto()
        {
            // Arrange
            var products = new List<Product>
            {
                _fakeData.GenerateFakeProduct(),
                _fakeData.GenerateFakeProduct(),
                _fakeData.GenerateFakeProduct()
            };

            var productsDto = _helper.GenerateDtoList(products);

            _helper.SetupGetAllAsync(products);
            _helper.SetupMapperForProductsListToDto(products, productsDto);

            // Act
            var result = await _catalogService.GetAllProductsAsync(_cancellationToken);

            // Assert
            result
                .Should().NotBeNull();
            result
                .Result
                .Should().HaveCount(3);
        }

        [Fact]
        public async Task GetByIdAsync_WhenProductIsFound_ShouldReturnProductDto()
        {
            // Arrange
            var product = _fakeData.GenerateFakeProduct();
            var productDto = _helper.GenerateDto(product);

            _helper.SetupGetByIdAsync(product);
            _helper.SetupMapperForProductToDto(product, productDto);

            // Act
            var result = await _catalogService.GetProductAsync(product.Id, _cancellationToken);

            // Assert
            result
                .Should().NotBeNull();
            result
                .Result.Id
                .Should().Be(product.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenProductIsNull_ShouldThrowProductNotFoundException()
        {
            // Arrange
            var productId = _fakeData.GenerateFakeProduct().Id;

            _helper.SetupGetByIdAsyncWhenNull();

            // Act
            var result = async () => await _catalogService.GetProductAsync(productId, _cancellationToken);

            // Assert
            await result
                .Should().ThrowAsync<ProductNotFoundException>();
        }
    }
}
