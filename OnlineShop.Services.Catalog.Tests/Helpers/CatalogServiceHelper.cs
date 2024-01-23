using AutoMapper;
using Moq;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;

namespace OnlineShop.Services.Catalog.Tests.Helpers
{
    public class CatalogServiceHelper
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public CatalogServiceHelper(
            Mock<IMapper> mockMapper,
            Mock<IProductRepository> mockProductRepositoryt)
        {
            _mockMapper = mockMapper;
            _mockProductRepository = mockProductRepositoryt;
        }

        public void SetupGetAllAsync(List<Product> bookings)
        {
            _mockProductRepository
                .Setup(repository => repository.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);
        }

        public List<ProductDto> GenerateDtoList(IEnumerable<Product> products)
        {
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                OptionalFields = product.OptionalFields,
            }).ToList();
        }

        public ProductDto GenerateDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                OptionalFields = product.OptionalFields,
            };
        }

        public void SetupMapperForProductsListToDto(IEnumerable<Product> products,
             IEnumerable<ProductDto> productsDto)
        {
            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<ProductDto>>(products))
                .Returns(productsDto);
        }

        public void SetupGetByIdAsync(Product product)
        {
            _mockProductRepository
                .Setup(repository => repository.GetAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);
        }

        public void SetupMapperForProductToDto(Product product,
            ProductDto productDto)
        {
            _mockMapper
                .Setup(mapper => mapper.Map<ProductDto>(product))
                .Returns(productDto);
        }

        public void SetupGetByIdAsyncWhenNull()
        {
            _mockProductRepository
                .Setup(repository => repository.GetAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null);
        }
    }
}
