using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductView?> CreateProduct(Guid categoryId, AddProductDto addProductDto);
        Task<ProductView?> UpdateProduct(Guid id, AddProductDto updateProductDto);
        Task<ProductView?> DeactivateProduct(Guid id);
        Task<ProductView?> RestoreProduct(Guid id);
        Task EraseProduct(Guid id);
        Task<IEnumerable<ProductView>?> GetAllProducts(bool? isActive, bool? inStock,
            string? filterOn, string? filterQuery, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<ProductView?> GetProductById(Guid id);
        Task<ProductView?> UnrestrictedGetProductById(Guid id);
    }
}
