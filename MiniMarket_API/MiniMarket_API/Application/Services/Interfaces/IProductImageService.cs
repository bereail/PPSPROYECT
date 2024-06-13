using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IProductImageService
    {
        Task<ProductImageView?> HandleImageUpload(Guid productId, AddProductImageDto addProductImage);
        Task<ProductImageBasicView?> HandleImageDeletion(Guid productId);
        Task<ProductImageBasicView?> GetProductImage(Guid productId);
    }
}
