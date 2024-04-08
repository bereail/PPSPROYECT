using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetails> CreateOrderDetailAsync(OrderDetails orderDetails);
        Task<OrderDetails?> UpdateDetailAsync(Guid id, OrderDetails orderDetails);
        //Task<OrderDetails?> DeactivateDetailAsync(Guid id);
        Task<OrderDetails?> EraseDetailAsync(Guid id);
        Task<IEnumerable<OrderDetails>> GetDetailsByOrderId(Guid orderId);
        Task<OrderDetails?> GetDetailByIdAsync(Guid id);
    }
}
