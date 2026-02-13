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
    }
}
