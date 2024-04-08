using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Guid id, Product product);
        Task<Product?> DeactivateProductAsync(Guid id);
        Task<Product?> EraseProductAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);

    }
}
