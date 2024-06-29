using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Guid id, Product product);
        Task<int> HandleProductStockAsync(Guid id, int newStock);
        Task<Product?> DeactivateProductAsync(Guid id);
        Task<Product?> RestoreProductAsync(Guid id);
        Task EraseProductAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool? isActive, bool? inStock, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 15);             //FOR SELLER/ADMIN ONLY
        Task<IEnumerable<Product>> GetAllCategoryProductsAsync(Guid categoryId, bool? isActive, bool? inStock,
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 15);
        Task<ICollection<Guid>> CascadeProductIds(Guid categoryId, DateTime filterTime);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<bool> CheckIfProductExistsAsync(string productName);
        Task<Product?> UnrestrictedGetByIdAsync(Guid id);       //FOR SELLER/ADMIN & RESTRICED METHOD USE ONLY
        Task<ICollection<Guid>> CategoryAllProductIds(Guid categoryId);

    }
}
