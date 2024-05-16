using MiniMarket_API.Application.DTOs;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ISaleOrderService
    {
        Task<SaleOrderDetailsDto?> CreateSaleOrder(CreateOrderDto createOrderDto, Guid userId);
        Task<SaleOrderDetailsDto?> CancelOrder(Guid id, int cancelStatus = 2);
        Task<SaleOrderDetailsDto?> PayOrder(Guid id, int paymentStatus = 1);
        Task<SaleOrderDto?> EraseOrder(Guid id);
        Task<IEnumerable<SaleOrderDto>?> GetAllOrders(string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByUser(Guid id, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByTimeframe(int? filterDays,
            string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<SaleOrderDetailsDto?> GetOrderById(Guid id);
        Task<Guid> CheckOrderUserId(Guid orderId);
    }
}
