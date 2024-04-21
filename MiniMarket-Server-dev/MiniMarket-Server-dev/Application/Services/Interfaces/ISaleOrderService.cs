using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Application.DTOs;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface ISaleOrderService
    {
        Task<SaleOrderDetailsDto?> CreateSaleOrder(CreateOrderDto createOrderDto);
        Task<SaleOrderDetailsDto?> DeactivateOrder(Guid id);
        Task<SaleOrderDto?> EraseOrder(Guid id);
        Task<IEnumerable<SaleOrderDto>?> GetAllOrders(bool? isActive, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<IEnumerable<SaleOrderDto>?> GetAllOrdersByUser(Guid id, bool? isActive, string? sortBy, bool? isAscending,
            int pageNumber, int pageSize);
        Task<SaleOrderDetailsDto?> GetOrderById(Guid id);
    }
}
