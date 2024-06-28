using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Data.Interfaces
{
    public interface ISaleOrderRepository
    {
        Task<SaleOrder> CreateOrderAsync(SaleOrder order);
        Task SetFinalOrderPriceAsync(Guid id, decimal finalPrice);
        Task<SaleOrder?> SetOrderStatusAsync(Guid id, Guid userId, int newStatus);
        Task<SaleOrder?> EraseOrderAsync(Guid id);
        Task<IEnumerable<SaleOrder>> GetAllOrders(OrderStatus? status, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId, OrderStatus? status, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 7);
        Task<IEnumerable<SaleOrder>> GetAllOrdersFromTimeframeAsync(DateTime filterTime, OrderStatus? status,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<SaleOrder?> GetOrderByIdAsync(Guid id);
        Task<Guid> CheckOrderUserIdAsync(Guid orderId);
        Task<ICollection<Guid>> GetPendingOrderIdsByUserIdAsync(Guid userId);
        Task<ICollection<Guid>> GetOrderIdsByUserIdAsync(Guid userId);
        Task TerminateOrderAddressRelationship(Guid id);
    }
}
