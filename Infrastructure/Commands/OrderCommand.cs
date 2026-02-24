using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class OrderCommand : IOrderCommand
    {
        private readonly AppDbContext _context;

        public OrderCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Order entity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateOrder(Order entity, CancellationToken cancellationToken = default)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
