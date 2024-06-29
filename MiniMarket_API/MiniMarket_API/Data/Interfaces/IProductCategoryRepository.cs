using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory category);
        Task<ProductCategory?> UpdateProductCategoryAsync(Guid id, ProductCategory category);
        Task<ProductCategory?> DeactivateProductCategoryAsync(Guid id);
        Task<ProductCategory?> RestoreProductCategoryAsync(Guid id);
        Task<DateTime?> CascadeRestoreProductCategoryAsync(Guid id);
        Task EraseProductCategoryAsync(Guid id);
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync(bool? isActive, string? sortBy = null, bool isAscending = true);
        Task<ProductCategory?> GetCategoryByIdAsync(Guid id);
        Task<bool> CheckIfCategoryExistsAsync(string categoryName);
    }
}
