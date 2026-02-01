using Application.Interfaces.Query;
using Application.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _context;

        public DishQuery(AppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Dish>> GetAllAsync(string? name, int? categoryId, SortDirection? sortByPrice, bool onlyActive, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Dish> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Dish?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Dishes
                .Include(dish => dish.CategoryNavigator)
                .AsNoTracking()
                .FirstOrDefaultAsync(dish => dish.DishId == id, cancellationToken);
        }

    }
}
