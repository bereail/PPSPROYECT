using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Guid id, Product product);
        Task<int> HandleProductStockAsync(Guid id, int newStock);
        Task<Product?> DeactivateProductAsync(Guid id);
        Task<Product?> EraseProductAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool? isActive, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 50);
        Task<Product?> GetProductByIdAsync(Guid id);

    }
}
