using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory category);
        Task<ProductCategory?> UpdateProductCategoryAsync(Guid id, ProductCategory category);
        Task<ProductCategory?> DeactivateProductCategoryAsync(Guid id);
        Task<ProductCategory?> EraseProductCategoryAsync(Guid id);
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
        Task<ProductCategory?> GetCategoryByIdAsync(Guid id);
    }
}
