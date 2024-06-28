using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetails> CreateOrderDetailAsync(OrderDetails orderDetails);
        Task EraseDetailAsync(Guid id);
        Task<OrderDetails?> GetDetailByIdAsync(Guid id);
        Task<ICollection<Guid>> GetDetailIdsByProductId(Guid productId);
        Task SetProductRelationshipNull(Guid id);
    }
}
