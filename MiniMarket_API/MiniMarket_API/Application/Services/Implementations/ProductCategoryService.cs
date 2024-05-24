﻿using AutoMapper;
using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductCategoryService(IProductCategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryDto> CreateProductCategory(AddCategoryDto addCategoryDto)
        {
            var categoryToCreate = mapper.Map<ProductCategory>(addCategoryDto);
            await _categoryRepository.CreateProductCategoryAsync(categoryToCreate);

            return mapper.Map<CategoryDto>(categoryToCreate);
        }

        public async Task<CategoryDto?> UpdateProductCategory(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var categoryToUpdate = mapper.Map<ProductCategory>(updateCategoryDto);
            categoryToUpdate = await _categoryRepository.UpdateProductCategoryAsync(id, categoryToUpdate);
            if (categoryToUpdate == null)
            {
                return null;
            }
            return mapper.Map<CategoryDto?>(categoryToUpdate);
        }

        public async Task<CategoryCollectionDto?> DeactivateProductCategory(Guid id)
        {
            var categoryToDeactivate = await _categoryRepository.DeactivateProductCategoryAsync(id);
            if (categoryToDeactivate == null)
            {
                return null;
            }

            ICollection<Product> productsToCascadeDeactivate = categoryToDeactivate.Products;

            foreach (Product product in productsToCascadeDeactivate)
            {
                await productRepository.DeactivateProductAsync(product.Id);
                continue;
            }

            return mapper.Map<CategoryCollectionDto?>(categoryToDeactivate);
        }

        public async Task<CategoryDto?> RestoreProductCategory(Guid id)
        {
            var categoryToRestore = await _categoryRepository.RestoreProductCategoryAsync(id);
            if (categoryToRestore == null)
            {
                return null;
            }
            return mapper.Map<CategoryDto?>(categoryToRestore);
        }

        public async Task<CategoryCollectionDto?> CascadeRestoreProductCategory(Guid id)
        {
            //Upon restoring the category, we will get it's former deletion time, so we can use it as a filter
            var newFilterTime = await _categoryRepository.CascadeRestoreProductCategoryAsync(id);
            if (newFilterTime == null)
            {
                return null;
            }

            //We pass the filterTime alongside the categoryId, to bring back all productIds deactivated with the category
            var productsToRestore = await productRepository.CascadeProductIds(id, newFilterTime.Value);

            //This is to have the category for the return statement
            var restoredCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            //We run a loop to restore each product previously filtered, and add it to the restored category for the return statement
            foreach (Guid productId in productsToRestore)
            {
                var restoredProduct = await productRepository.RestoreProductAsync(productId);
                if (restoredProduct == null) { continue; }
                restoredCategory.Products.Add(restoredProduct);
            }

            return mapper.Map<CategoryCollectionDto>(restoredCategory);
        }

        public async Task<CategoryDto?> EraseProductCategory(Guid id)
        {
            var categoryToErase = await _categoryRepository.EraseProductCategoryAsync(id);
            if (categoryToErase == null)
            {
                return null;
            }
            return mapper.Map<CategoryDto?>(categoryToErase);
        }

        public async Task<IEnumerable<CategoryDto>?> GetAllCategories(bool? isActive, string? sortBy, bool? isAscending)
        {
            var categories = await _categoryRepository.GetAllProductCategoriesAsync(isActive, sortBy, isAscending ?? true);
            if (!categories.Any())
            {
                return null;
            }
            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryCollectionDto?> GetCategoryCollection(Guid categoryId, bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1) { pageSize = 1; }

            var getCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (getCategory == null)
            {
                return null;
            }
            var getProducts = await productRepository.GetAllCategoryProductsAsync(categoryId, isActive, filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            return mapper.Map<CategoryCollectionDto?>(getCategory);
        }
    }
}
