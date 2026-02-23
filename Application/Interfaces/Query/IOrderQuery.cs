using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderQuery
    {
        Task<IReadOnlyList<Order>> GetAllAsync(DateTime? from, DateTime? to, int? status, CancellationToken cancellation = default);
        Task<Order> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
