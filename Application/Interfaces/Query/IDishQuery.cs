using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IDishQuery
    {
        Task<IReadOnlyList<Dish>> GetAllAsync(string? Name, int? CategoryId, SortDirection? SortByPrice, bool OnlyActive, CancellationToken cancellationToken = default);
        Task<Dish> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsOtherWithNameAsync(string name, Guid dishId, CancellationToken cancellationToken = default);
        Task<Dish?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
