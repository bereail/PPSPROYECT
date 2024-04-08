using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Data.Repositories
{
    public class SaleOrderRepository : ISaleOrderRepository
    {
        private readonly MarketDbContext _context;

        public SaleOrderRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<SaleOrder> CreateOrderAsync(SaleOrder order)
        {
            order.Id = Guid.NewGuid();
            order.OrderTime = DateTime.UtcNow;
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<SaleOrder?> UpdateOrderAsync(Guid id, SaleOrder order)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (getOrder == null) 
            {
                return null;
            }
            getOrder.PaymentMethod = order.PaymentMethod;
            getOrder.Details = order.Details;
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<SaleOrder?> DeactivateOrderAsync (Guid id)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (getOrder == null)
            {
                return null;
            }
            getOrder.IsActive = false;
            getOrder.DeactivationTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<SaleOrder?> EraseOrderAsync (Guid id)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && !o.IsActive);
            if (getOrder == null)
            {
                return null;
            }
            _context.Orders.Remove(getOrder);
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId)
        {
            return await 
                _context.Orders
                .Where(o => o.UserId == userId && o.IsActive)
                .ToListAsync();
        }

        //public async Task<IEnumerable<SaleOrder>> GetAllOrdersFromTodayAsync (DateTime today)             
        //{

        //}

        public Task<SaleOrder?> GetOrderByIdAsync(Guid id)
        {
            return _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        } 
    }
}
