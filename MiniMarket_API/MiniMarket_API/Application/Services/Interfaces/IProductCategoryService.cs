using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<CategoryDto> CreateProductCategory(AddCategoryDto addCategoryDto);
        Task<CategoryDto?> UpdateProductCategory(Guid id, UpdateCategoryDto updateCategoryDto);
        Task<CategoryCollectionDto?> DeactivateProductCategory(Guid id);
        Task<CategoryDto?> RestoreProductCategory(Guid id);
        Task<CategoryCollectionDto?> CascadeRestoreProductCategory(Guid id);
        Task<CategoryDto?> EraseProductCategory(Guid id);
        Task<IEnumerable<CategoryDto>?> GetAllCategories(bool? isActive, string? sortBy, bool? isAscending);
        Task<CategoryCollectionDto?> GetCategoryCollection(Guid categoryId, bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize);
    }
}
