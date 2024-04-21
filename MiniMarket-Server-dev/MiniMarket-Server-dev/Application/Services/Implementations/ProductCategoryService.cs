using AutoMapper;
using MiniMarket_Server_dev.Application.DTOs;
using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.Services.Interfaces;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IMapper mapper;

        public ProductCategoryService(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
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

        public async Task<CategoryDto?> DeactivateProductCategory(Guid id)
        {
            var categoryToDeactivate = await _categoryRepository.DeactivateProductCategoryAsync(id);
            if (categoryToDeactivate == null)
            {
                return null;
            }
            return mapper.Map<CategoryDto?>(categoryToDeactivate);
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

        public async Task<CategoryCollectionDto?> GetCategoryCollection(Guid id)
        {
            var getCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (getCategory == null)
            {
                return null;
            }
            return mapper.Map<CategoryCollectionDto?>(getCategory);
        }
    }
}
