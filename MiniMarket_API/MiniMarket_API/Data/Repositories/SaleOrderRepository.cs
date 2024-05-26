using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Model.Enums;

namespace MiniMarket_API.Data.Repositories
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

        public async Task SetFinalOrderPriceAsync(Guid id, decimal finalPrice)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            getOrder.FinalPrice = finalPrice;
            await _context.SaveChangesAsync();
        }

        public async Task<SaleOrder?> SetOrderStatusAsync(Guid id, int newStatus)
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

        public async Task<SaleOrder?> EraseOrderAsync(Guid id)
        {
            var getOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && o.Status != (OrderStatus)1);
            if (getOrder == null)
            {
                return null;
            }
            _context.Orders.Remove(getOrder);
            await _context.SaveChangesAsync();
            return getOrder;
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrders(OrderStatus? status, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var orders = _context.Orders.AsQueryable();

            if (status.HasValue && Enum.IsDefined(typeof(OrderStatus), status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.OrderTime) : orders.OrderBy(o => o.OrderTime);
                }

                if (sortBy.Equals("FinishDate", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.FinishTime) : orders.OrderBy(o => o.FinishTime);
                }

                if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersByUserAsync(Guid userId, OrderStatus? status, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 7)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId).AsQueryable();

            if (status.HasValue && Enum.IsDefined(typeof(OrderStatus), status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.OrderTime) : orders.OrderBy(o => o.OrderTime);
                }

                if (sortBy.Equals("FinishDate", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.FinishTime) : orders.OrderBy(o => o.FinishTime);
                }

                if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<SaleOrder>> GetAllOrdersFromTimeframeAsync(DateTime filterTime, OrderStatus? status,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var orders = _context.Orders.AsQueryable();

            orders = orders.Where(o => o.OrderTime >= filterTime);

            if (status.HasValue && Enum.IsDefined(typeof(OrderStatus), status))
            {
                orders = orders.Where(o => o.Status == status);
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.OrderTime) : orders.OrderBy(o => o.OrderTime);
                }

                if (sortBy.Equals("FinishDate", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderByDescending(o => o.FinishTime) : orders.OrderBy(o => o.FinishTime);
                }

                if (sortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAscending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status);
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

        public async Task<Guid> CheckOrderUserIdAsync(Guid orderId) 
        { 
            var getOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (getOrder == null) { return Guid.Empty; }
            return getOrder.UserId;
        }
    }
}
