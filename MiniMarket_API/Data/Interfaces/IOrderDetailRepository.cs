using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetails> CreateOrderDetailAsync(OrderDetails orderDetails);
        Task<OrderDetails?> UpdateDetailAsync(Guid id, OrderDetails orderDetails);
        //Task<OrderDetails?> DeactivateDetailAsync(Guid id);
        Task<Guid?> EraseDetailAsync(Guid id);
        Task<IEnumerable<OrderDetails>> GetDetailsByOrderId(Guid orderId);
        Task<OrderDetails?> GetDetailByIdAsync(Guid id);
        Task<OrderDetails?> GetDetailByOrderProductId(Guid orderId, Guid productId);
    }
}
