using AutoMapper;
using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductDto?> CreateProduct(AddProductDto addProductDto)
        {
            var checkCategory = await _productCategoryRepository.GetCategoryByIdAsync(addProductDto.CategoryId);
            if (checkCategory == null)
            {
                return null;
            }
            var productToCreate = _mapper.Map<Product>(addProductDto);
            await _productRepository.CreateProductAsync(productToCreate);
            return _mapper.Map<ProductDto>(productToCreate);
        }

        public async Task<ProductDto?> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var productToUpdate = _mapper.Map<Product>(updateProductDto);
            productToUpdate = await _productRepository.UpdateProductAsync(id, productToUpdate);

            if (productToUpdate == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto?>(productToUpdate);
        }

        public async Task<ProductDto?> DeactivateProduct(Guid id)
        {
            var productToDeactivate = await _productRepository.DeactivateProductAsync(id);
            if (productToDeactivate == null)
            {
                return null;
            }

            return _mapper.Map<ProductDto?>(productToDeactivate);
        }

        public async Task<ProductDto?> EraseProduct(Guid id)
        {
            var productToErase = await _productRepository.EraseProductAsync(id);
            if (productToErase == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto?>(productToErase);
        }

        public async Task<IEnumerable<ProductDto>?> GetAllProducts(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetAllProductsAsync(isActive, filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);    //If the bool is null, it's changed to true.
            if (!products.Any())
            {
                return null;
            }
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductById(Guid id)
        {
            var getProduct = await _productRepository.GetProductByIdAsync(id);
            if (getProduct == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto?>(getProduct);
        }
    }
}
