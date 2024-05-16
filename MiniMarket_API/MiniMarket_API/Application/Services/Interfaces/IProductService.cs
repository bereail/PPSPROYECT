using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.DTOs;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> CreateProduct(AddProductDto addProductDto);
        Task<ProductDto?> UpdateProduct(Guid id, UpdateProductDto updateProductDto);
        Task<ProductDto?> DeactivateProduct(Guid id);
        Task<ProductDto?> RestoreProduct(Guid id);
        Task<ProductDto?> EraseProduct(Guid id);
        Task<IEnumerable<ProductDto>?> GetAllProducts(bool? isActive, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<ProductDto?> GetProductById(Guid id);
    }
}
