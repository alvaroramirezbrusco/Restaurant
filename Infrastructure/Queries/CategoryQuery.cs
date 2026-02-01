using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly AppDbContext _context;

        public CategoryQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Categories.AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FindAsync(Id);
        }
    }
}
