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

        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var response = new ResponseDto<IEnumerable<ProductDto>>() { Result = productDtos };

            return response;
        }

        public async Task<ResponseDto<ProductDto>> GetProductAsync(string id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }

            var productDto = _mapper.Map<ProductDto>(product);
            var response = new ResponseDto<ProductDto>() { Result = productDto };

            return response;
        }

        public async Task<ResponseDto<string>> AddProductAsync(NewProductDto productDto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productDto);
            var id = await _productRepository.AddAsync(product, cancellationToken);
            var response = new ResponseDto<string>() { Message = "Product added successfully", Result = id };

            return response;
        }

        public async Task<ResponseDto<object>> UpdateProductAsync(string id, NewProductDto productDto, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetAsync(id, cancellationToken);

            if (existingProduct is null)
            {
                throw new ProductNotFoundException(id);
            }

            var product = _mapper.Map(productDto, existingProduct);
            await _productRepository.UpdateAsync(product, cancellationToken);

            var response = new ResponseDto<object>() { Message = "Product updated successfully" };

            return response;
        }

        public async Task<ResponseDto<object>> DeleteProductAsync(string id, CancellationToken cancellationToken)
        {
            var isSuccess = await _productRepository.DeleteAsync(id, cancellationToken);

            if (!isSuccess)
            {
                throw new ProductNotFoundException(id);
            }

            var response = new ResponseDto<object>() { Message = "Product deleted successfully" };

            return response;
        }
    }
}