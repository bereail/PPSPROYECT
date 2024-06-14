using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IOrderDetailsService
    {
        Task<OrderDetails?> CreateOrderDetail(CreateDetailDto createDetail, Guid orderId);
        Task EraseOrderDetail(Guid id);
    }
}
