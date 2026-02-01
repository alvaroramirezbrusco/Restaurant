using Domain.Entities;

namespace Application.Interfaces.Command
{
    public interface IDishCommand
    {
        Task InsertAsync(Dish entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Dish entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Dish entity, CancellationToken cancellationToken = default);
    }
}
