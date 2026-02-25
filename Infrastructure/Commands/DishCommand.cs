using Application.Interfaces.Command;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Commands
{
    public class DishCommand : IDishCommand
    {
        private readonly AppDbContext _context;

        public DishCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Dish entity, CancellationToken cancellationToken = default)
        {
            _context.Dishes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(Dish entity, CancellationToken cancellationToken = default)
        {
            await _context.Dishes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Dish entity, CancellationToken cancellationToken = default)
        {
            _context.Dishes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
