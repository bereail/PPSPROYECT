using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Data.Repositories
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

        public async Task EraseDetailAsync(Guid id)
        {
            var getDetailToErase = await _context.Details.FirstOrDefaultAsync(x => x.Id == id);
            if (getDetailToErase == null)
            {
                return;
            }
            _context.Details.Remove(getDetailToErase);
            await _context.SaveChangesAsync();
        }

        public Task<OrderDetails?> GetDetailByIdAsync(Guid id)
        {
            return _context.Details
                .Include(d => d.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Guid>> GetDetailIdsByProductId(Guid productId)
        {
            var idCollection = await _context.Details
                .Where(x => x.ProductId == productId)
                .Select(x => x.Id)
                .ToListAsync();
            return idCollection;
        }

        public async Task SetProductRelationshipNull(Guid id)
        {
            var getDetailToUpdate = await _context.Details.FirstOrDefaultAsync(d => d.Id == id);

            if (getDetailToUpdate == null) { return; }

            getDetailToUpdate.ProductId = null;

            await _context.SaveChangesAsync();
        }
    }
}
