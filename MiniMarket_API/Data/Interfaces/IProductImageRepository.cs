using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IProductImageRepository
    {
        Task<ProductImage?> UploadImageAsync(ProductImage image);
        Task<ProductImage?> GetImageByProductAsync(Guid productId);
        Task<ProductImage?> DeleteImagebyIdAsync(Guid id);
    }
}
