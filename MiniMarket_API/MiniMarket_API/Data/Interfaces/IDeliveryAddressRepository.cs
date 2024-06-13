using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Interfaces
{
    public interface IDeliveryAddressRepository
    {
        Task<DeliveryAddress> CreateDeliveryAddressAsync(DeliveryAddress deliveryAddress);
        Task<DeliveryAddress?> UpdateDeliveryAddressAsync(Guid id, DeliveryAddress deliveryAddress);
        Task<DeliveryAddress?> DeleteDeliveryAddressAsync(Guid id);
        Task<DeliveryAddress?> GetAddressByUserId(Guid userId);
        Task<Guid> GetAddressIdByUserId(Guid userId);
    }
}
