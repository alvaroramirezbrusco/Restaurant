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

        public async Task DeleteAsync(Dish dish, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Dish dish, CancellationToken cancellationToken = default)
        {
            await _context.Dishes.AddAsync(dish, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Dish dish, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
