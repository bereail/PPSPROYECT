using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
{
    public class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private readonly MarketDbContext _context;

        public DeliveryAddressRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryAddress> CreateDeliveryAddressAsync(DeliveryAddress deliveryAddress)
        {
            deliveryAddress.Id = Guid.NewGuid();
            await _context.AddAsync(deliveryAddress);
            await _context.SaveChangesAsync();

            return deliveryAddress;
        }

        public async Task<DeliveryAddress?> UpdateDeliveryAddressAsync(Guid id, DeliveryAddress deliveryAddress)
        {
            var getAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync(x => x.Id == id);
            if (getAddress == null)
            {
                return null;
            }
            getAddress.Province = deliveryAddress.Province;
            getAddress.City = deliveryAddress.City;
            getAddress.Street = deliveryAddress.Street;
            getAddress.Floor = deliveryAddress.Floor;
            getAddress.Apartment = deliveryAddress.Apartment;

            await _context.SaveChangesAsync();

            return getAddress;
        }

        public async Task<DeliveryAddress?> DeleteDeliveryAddressAsync(Guid id)
        {
            var getAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync(x => x.Id == id);

            if (getAddress == null)
            {
                return null;
            }
            _context.DeliveryAddresses.Remove(getAddress);
            await _context.SaveChangesAsync();
            return getAddress;
        }

        public async Task<DeliveryAddress?> GetAddressByUserId(Guid userId)
        {
            var getAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync(x =>x.UserId == userId);

            if (getAddress == null)
            { return null; }

            return getAddress;
        }

        public async Task<Guid> GetAddressIdByUserId(Guid userId)
        {
            var addressId = await _context.DeliveryAddresses
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return addressId;
        }
    }
}
