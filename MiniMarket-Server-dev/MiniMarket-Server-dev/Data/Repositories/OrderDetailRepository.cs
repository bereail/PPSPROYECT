using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly MarketDbContext _context;

        public OrderDetailRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDetails> CreateOrderDetailAsync(OrderDetails orderDetails)
        {
            orderDetails.Id = Guid.NewGuid();
            await _context.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
            return orderDetails;
        }

        public async Task<OrderDetails?> UpdateDetailAsync(Guid id,OrderDetails orderDetails)
        {
            var getDetails = await _context.Details.FirstOrDefaultAsync(x => x.Id == id);
            if (getDetails == null) 
            {
                return null;
            }
            getDetails.ProductQuantity = orderDetails.ProductQuantity;

            await _context.SaveChangesAsync();
            return getDetails;
        }

        public async Task<OrderDetails?> DeactivateDetailAsync(Guid id)           //Will be implemented when OrderDetails receives the IsActive attribute.
        {
            var getDetailToDeactivate = await _context.Details.FirstOrDefaultAsync(x => x.Id == id);
            if (getDetailToDeactivate == null)
            {
                return null;
            }
            getDetailToDeactivate.isActive = false;
            return getDetailToDeactivate;
        }

        public async Task<OrderDetails?> EraseDetailAsync(Guid id)
        {
            var getDetailToErase = await _context.Details.FirstOrDefaultAsync(x => x.Id == id);
            if (getDetailToErase == null)
            {
                return null;
            }
            _context.Details.Remove(getDetailToErase);
            await _context.SaveChangesAsync();
            return getDetailToErase;
        }

        public async Task<IEnumerable<OrderDetails>> GetDetailsByOrderId(Guid orderId)      //Could theoretically be swapped for a .Include(); in SaleOrderRepository
        {
            return await
                _context.Details
                .Where(d => d.OrderId == orderId)
                .ToListAsync();
        }

        public Task<OrderDetails?> GetDetailByIdAsync(Guid id)
        {
            return _context.Details
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
