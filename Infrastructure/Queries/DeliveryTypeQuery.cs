using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class DeliveryTypeQuery : IDeliveryTypeQuery
    {
        private readonly AppDbContext _context;

        public DeliveryTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<DeliveryType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.DeliveryTypes.AsQueryable();
            return await query.ToListAsync();
        }
    }
}
