using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IDishQuery
    {
        Task<IEnumerable<Dish>> GetAllAsync(string? name, int? categoryId, SortDirection? sortByPrice, bool onlyActive, CancellationToken cancellationToken = default);
        Task<Dish> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Dish?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
