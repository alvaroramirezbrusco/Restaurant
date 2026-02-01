using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface ICategoryQuery
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    }
}
