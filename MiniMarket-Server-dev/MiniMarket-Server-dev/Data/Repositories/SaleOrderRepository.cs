using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Data.Interfaces;
using MiniMarket_Server_dev.Model;
using MiniMarket_Server_dev.Model.Entities;
using MiniMarket_Server_dev.Model.Enums;

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
            var getOrder = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (getOrder == null) 
            {
                return null;
            }
            getOrder.DeliveryAddress = order.DeliveryAddress;
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task SetFinalOrderPriceAsync (Guid id, decimal finalPrice)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            
            getOrder.FinalPrice = finalPrice;
            await _context.SaveChangesAsync();
        } 

        public async Task<SaleOrder?> SetOrderStatusAsync (Guid id, int newStatus)
        {
            var getOrder = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id && o.Status == 0);
            if (getOrder == null)
            {
                return null;
            }
            getOrder.Status = (OrderStatus)newStatus;
            getOrder.FinishTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<SaleOrder?> EraseOrderAsync (Guid id)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (getOrder == null)
            {
                return null;
            }
            _context.Orders.Remove(getOrder);
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrders(string? sortBy = null, bool isAscending = true, 
            int pageNumber = 1, int pageSize = 30)
        {
            var orders = _context.Orders.AsQueryable();

            //if (isActive != null)
            //{
            //    orders = isActive.Value ? orders.Where(o => o.IsActive) : orders.Where(o => !o.IsActive);
            //}

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.OrderTime) : orders.OrderByDescending(o => o.OrderTime);
                }

                if (sortBy.Equals("FinishDate", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.FinishTime) : orders.OrderByDescending(o => o.FinishTime);
                }

                if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 30)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId).AsQueryable();

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.OrderTime) : orders.OrderByDescending(o => o.OrderTime);
                }

                if (sortBy.Equals("FinishDate", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.FinishTime) : orders.OrderByDescending(o => o.FinishTime);
                }

                if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersFromTimeframeAsync (DateTime filterTime,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 30)             
        {
            var orders = _context.Orders.AsQueryable();

            //DateTime filterTime = DateTime.Now.AddDays(-filterDays);

            orders = orders.Where(o => o.OrderTime >= filterTime);

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

        public Task<SaleOrder?> GetOrderByIdAsync(Guid id)
        {
            return _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);
        } 
    }
}
