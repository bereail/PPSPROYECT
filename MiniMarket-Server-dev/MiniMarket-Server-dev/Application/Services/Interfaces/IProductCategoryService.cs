using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.DTOs;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<CategoryDto> CreateProductCategory(AddCategoryDto addCategoryDto);
        Task<CategoryDto?> UpdateProductCategory(Guid id, UpdateCategoryDto updateCategoryDto);
        Task<CategoryDto?> DeactivateProductCategory(Guid id);
        Task<CategoryDto?> EraseProductCategory(Guid id);
        Task<IEnumerable<CategoryDto>?> GetAllCategories(bool? isActive, string? sortBy, bool? isAscending);
        Task<CategoryCollectionDto?> GetCategoryCollection(Guid id);
    }
}
