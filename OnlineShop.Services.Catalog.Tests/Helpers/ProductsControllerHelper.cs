using Moq;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using OnlineShop.Services.Catalog.Tests.FakeData;

namespace OnlineShop.Services.Catalog.Tests.Helpers
{
    public class ProductsControllerHelper
    {
        private readonly Mock<ICatalogService> _mockCatalogService;

        public ProductsControllerHelper(Mock<ICatalogService> mockBookingService)
        {
            _mockCatalogService = mockBookingService;
        }

        public void SetupGetAllAsync(IEnumerable<ProductDto> products)
        {
            _mockCatalogService
                .Setup(service => service.GetAllProductsAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new ResponseDto<IEnumerable<ProductDto>>()
                    {
                        Result = products
                    });
        }

        public void SetupGetByIdAsync(ProductDto product)
        {
            _mockCatalogService
                .Setup(service => service.GetProductAsync(
                    product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new ResponseDto<ProductDto>()
                    {
                        Result = product
                    });
        }

        public void SetupAddAsync(NewProductDto product)
        {
            _mockCatalogService
                .Setup(service => service.AddProductAsync(
                    product, It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new ResponseDto<string>()
                    {
                        Result = new CatalogFakeDataGenerator().GenerateFakeProductId()
                    });
        }

        public void SetupUpdateAsync(string id, NewProductDto productDto)
        {
            _mockCatalogService
                .Setup(service => service.UpdateProductAsync(
                    id, productDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ResponseDto());
        }

        public void SetupDeleteAsync(string id)
        {
            _mockCatalogService
                .Setup(service => service.DeleteProductAsync(
                    id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ResponseDto());
        }
    }
}
