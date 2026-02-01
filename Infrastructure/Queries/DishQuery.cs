using Application.Interfaces.Query;
using Application.Models;
using Domain.Constants;
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

        public async Task<IReadOnlyList<Dish>> GetAllAsync(string? Name, int? CategoryId, SortDirection? SortByPrice, bool OnlyActive, CancellationToken cancellationToken = default)
        {
            var query = _context.Dishes
                .Include(dish => dish.CategoryNavigator)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(d => d.Name.Contains(Name));
            }
            if (CategoryId.HasValue)
            {
                query = query.Where(d => d.Category == CategoryId);
            }
            if (OnlyActive)
            {
                query = query.Where(d => d.Available);
            }
            if (SortByPrice.HasValue)
            {
                query = SortByPrice == SortDirection.asc
                    ? query.OrderBy(d => d.Price)
                    : query.OrderByDescending(d => d.Price);
            }

            return await query.ToListAsync(cancellationToken);
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
