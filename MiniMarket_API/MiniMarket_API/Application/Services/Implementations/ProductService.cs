﻿using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.ViewModels;
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

        public async Task<ProductView?> CreateProduct(Guid categoryId, AddProductDto addProductDto)
        {
            var checkCategory = await _productCategoryRepository.GetCategoryByIdAsync(categoryId);
            if (checkCategory == null)
            {
                throw new BadHttpRequestException("Product Creation Failed: Category Wasn't Found or is Currently Inactive");
            }
            var checkProductName = await _productRepository.CheckIfProductExistsAsync(addProductDto.Name);

            if (checkProductName != Guid.Empty)
            {
                return null;
            }

            var productToCreate = _mapper.Map<Product>(addProductDto);
            await _productRepository.CreateProductAsync(productToCreate);
            return _mapper.Map<ProductView>(productToCreate);
        }

        public async Task<ProductView?> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var productToUpdate = _mapper.Map<Product>(updateProductDto);
            productToUpdate = await _productRepository.UpdateProductAsync(id, productToUpdate);

            if (productToUpdate == null)
            {
                return null;
            }
            return _mapper.Map<ProductView?>(productToUpdate);
        }

        public async Task<ProductView?> DeactivateProduct(Guid id)
        {
            var productToDeactivate = await _productRepository.DeactivateProductAsync(id);
            if (productToDeactivate == null)
            {
                return null;
            }

            return _mapper.Map<ProductView?>(productToDeactivate);
        }

        public async Task<ProductView?> RestoreProduct(Guid id)
        {
            var productToRestore = await _productRepository.RestoreProductAsync(id);
            if (productToRestore == null)
            {
                return null;
            }
            return _mapper.Map<ProductView?>(productToRestore);
        }

        public async Task<ProductView?> EraseProduct(Guid id)
        {
            var productToErase = await _productRepository.EraseProductAsync(id);
            if (productToErase == null)
            {
                return null;
            }
            return _mapper.Map<ProductView?>(productToErase);
        }

        public async Task<IEnumerable<ProductView>?> GetAllProducts(bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1 || pageSize > 30) { pageSize = 15; }

            var products = await _productRepository.GetAllProductsAsync(isActive, filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);    //If the bool is null, it's changed to true.
            if (!products.Any())
            {
                return null;
            }
            return _mapper.Map<IEnumerable<ProductView>>(products);
        }

        public async Task<ProductView?> GetProductById(Guid id)
        {
            var getProduct = await _productRepository.GetProductByIdAsync(id);
            if (getProduct == null)
            {
                return null;
            }
            return _mapper.Map<ProductView?>(getProduct);
        }
    }
}
