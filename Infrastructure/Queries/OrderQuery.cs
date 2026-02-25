using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly AppDbContext _context;

        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(DateTime? from, DateTime? to, int? status, CancellationToken cancellation = default)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.DishNavigator)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.StatusNavigator)
                .Include(o => o.OverallStatusNavigation)
                .Include(o => o.DeliveryTypeNavigator)
                .AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(o => o.CreateDate >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(o => o.CreateDate <= to);
            }
            if (status.HasValue)
            {
                query = query.Where(o => o.OverallStatus == status.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.DeliveryTypeNavigator)
                .Include(o => o.OverallStatusNavigation)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.DishNavigator)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.StatusNavigator)
                  .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<bool> IsDishInActiveOrder(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AnyAsync(o => o.OrderItems.Any(oi => oi.Dish == id));
        }
    }
}
