using AutoMapper;
using OnlineShop.Services.Catalog.Application.Exceptions;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using OnlineShop.Services.Catalog.Domain.Models.Data;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;

namespace OnlineShop.Services.Catalog.Application.Services.Implementations
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CatalogService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var response = new ResponseDto() { Result = productDtos };

            return response;
        }

        public async Task<ResponseDto> GetProductAsync(string id)
        {
            var product = await _productRepository.GetAsync(id)
                ?? throw new ProductNotFoundException(id);
            var productDto = _mapper.Map<ProductDto>(product);
            var response = new ResponseDto() { Result = productDto };

            return response;
        }

        public async Task<ResponseDto> GetProductsByCategoryAsync(string category)
        {
            var products = await _productRepository.GetByCategoryAsync(category);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var response = new ResponseDto() { Result = productDtos };

            return response;
        }

        public async Task<ResponseDto> AddProductAsync(AddProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var id = await _productRepository.AddAsync(product);
            var response = new ResponseDto() { Message = "Product added successfully", Result = new { id } };

            return response;
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var isSuccess = await _productRepository.UpdateAsync(product);

            if (!isSuccess)
            {
                throw new ProductNotFoundException(productDto.Id);
            }

            var response = new ResponseDto() { Message = "Product updated successfully" };

            return response;
        }

        public async Task<ResponseDto> DeleteProductAsync(string id)
        {
            var isSuccess = await _productRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                throw new ProductNotFoundException(id);
            }

            var response = new ResponseDto() { Message = "Product deleted successfully" };

            return response;
        }
    }
}