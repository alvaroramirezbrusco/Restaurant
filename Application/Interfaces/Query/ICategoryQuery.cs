using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface ICategoryQuery
    {
        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    }
}
