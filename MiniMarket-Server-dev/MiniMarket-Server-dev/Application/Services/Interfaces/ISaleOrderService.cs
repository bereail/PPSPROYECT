using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.DTOs;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface ISaleOrderService
    {
        Task<SaleOrderDetailsDto?> CreateSaleOrder(CreateOrderDto createOrderDto);
        Task<SaleOrderDetailsDto?> UpdateSaleOrder(Guid orderId, UpdateOrderDto updateOrder);
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
    }
}
