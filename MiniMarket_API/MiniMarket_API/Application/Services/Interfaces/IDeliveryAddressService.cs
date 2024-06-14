using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface IDeliveryAddressService
    {
        Task<DeliveryAddressView?> AddDeliveryAddress(Guid userId, AddDeliveryAddressDto addDeliveryAddress);
        Task DeleteDeliveryAddress(Guid userId);
        Task<DeliveryAddressView?> GetDeliveryAddressByUserId(Guid userId);

    }
}
