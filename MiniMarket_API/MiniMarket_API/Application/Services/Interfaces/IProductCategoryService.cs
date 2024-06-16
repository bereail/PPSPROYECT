using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<CategoryView> CreateProductCategory(AddCategoryDto addCategoryDto);
        Task<CategoryView?> UpdateProductCategory(Guid id, AddCategoryDto updateCategoryDto);
        Task<CategoryViewProducts?> DeactivateProductCategory(Guid id);
        Task<CategoryView?> RestoreProductCategory(Guid id);
        Task<CategoryViewProducts?> CascadeRestoreProductCategory(Guid id);
        Task EraseProductCategory(Guid id);
        Task<IEnumerable<CategoryView>?> GetAllCategories(bool? isActive, string? sortBy, bool? isAscending);
        Task<CategoryViewProducts?> GetCategoryCollection(Guid categoryId, bool? isActive, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize);
    }
}
