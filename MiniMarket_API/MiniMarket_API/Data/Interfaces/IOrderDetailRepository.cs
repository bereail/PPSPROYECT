using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetails> CreateOrderDetailAsync(OrderDetails orderDetails);
        Task EraseDetailAsync(Guid id);
        Task<IEnumerable<OrderDetails>> GetDetailsByOrderId(Guid orderId);
        Task<OrderDetails?> GetDetailByIdAsync(Guid id);
        Task<OrderDetails?> GetDetailByOrderProductId(Guid orderId, Guid productId);
    }
}
