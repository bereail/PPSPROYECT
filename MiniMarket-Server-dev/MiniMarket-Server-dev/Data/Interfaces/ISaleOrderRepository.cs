using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Interfaces
{
    public interface ISaleOrderRepository
    {
        Task<SaleOrder> CreateOrderAsync(SaleOrder order);
        Task<SaleOrder?> UpdateOrderAsync(Guid id, SaleOrder order);
        Task<SaleOrder?> DeactivateOrderAsync(Guid id);
        Task<SaleOrder?> EraseOrderAsync(Guid id);
        Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId);
        //Task<IEnumerable<SaleOrder>> GetAllOrdersFromTodayAsync(DateTime today);
        Task<SaleOrder?> GetOrderByIdAsync(Guid id);
    }
}
