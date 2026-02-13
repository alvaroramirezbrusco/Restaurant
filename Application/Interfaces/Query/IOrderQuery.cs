using Domain.Entities;

namespace Application.Interfaces.Query
{
    public interface IOrderQuery
    {

        Task<Order> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
