using MiniMarket_API.Application.DTOs.Preferences;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ISaleOrderService
    {
        Task<SaleOrderViewDetails?> CreateSaleOrder(CreateOrderDto createOrderDto, Guid userId);
        Task<SaleOrderViewDetails?> CancelOrder(Guid id, Guid userId);
        Task<PreferenceData?> HandlePaymentRequest(Guid id, Guid userId);
        Task<SaleOrderView?> SetPaidOrderStatus(Guid id, Guid userId);
        Task<SaleOrderView?> EraseOrder(Guid id);
        Task<IEnumerable<SaleOrderView>?> GetAllOrders(OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<IEnumerable<SaleOrderView>?> GetAllOrdersByUser(Guid id, OrderStatus? status, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<IEnumerable<SaleOrderView>?> GetAllOrdersByTimeframe(int? filterDays, OrderStatus? status,
            string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<SaleOrderViewDetails?> GetOrderById(Guid id);
        Task<Guid> CheckOrderUserId(Guid orderId);
    }
}
