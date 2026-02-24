using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IOrderCommand
    {
        Task InsertAsync(Order entity, CancellationToken cancellationToken = default);
        Task UpdateOrder(Order entity, CancellationToken cancellationToken = default);
    }
}
