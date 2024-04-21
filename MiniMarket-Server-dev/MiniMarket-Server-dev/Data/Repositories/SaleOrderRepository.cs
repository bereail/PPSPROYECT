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

        public async Task<SaleOrder?> UpdateOrderAsync(Guid id, SaleOrder order)                //Will be properly implemented later.
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

        public async Task SetFinalOrderPriceAsync (Guid id, decimal finalPrice)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            
            getOrder.FinalPrice = finalPrice;
            await _context.SaveChangesAsync();
        } 

        public async Task<SaleOrder?> DeactivateOrderAsync (Guid id)
        {
            var getOrder = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id && o.IsActive);
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

        public async Task<IEnumerable<SaleOrder>> GetAllOrders(bool? isActive, string? sortBy = null, bool isAscending = true, 
            int pageNumber = 1, int pageSize = 30)
        {
            var orders = _context.Orders.AsQueryable();

            if (isActive != null)
            {
                orders = isActive.Value ? orders.Where(o => o.IsActive) : orders.Where(o => !o.IsActive);
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.OrderTime) : orders.OrderByDescending(o => o.OrderTime);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId, bool? isActive, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 30)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId).AsQueryable();

            if (isActive != null)
            {
                orders = isActive.Value ? orders.Where(o => o.IsActive) : orders.Where(o => !o.IsActive);
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.OrderTime) : orders.OrderByDescending(o => o.OrderTime);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        //public async Task<IEnumerable<SaleOrder>> GetAllOrdersFromTodayAsync (DateTime today)             
        //{

        //}

        public Task<SaleOrder?> GetOrderByIdAsync(Guid id)
        {
            return _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);
        } 
    }
}
