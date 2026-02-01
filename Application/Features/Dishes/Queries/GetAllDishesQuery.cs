using Application.Models;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Queries
{
    public record GetAllDishesQuery(string? name, int? categoryId, SortDirection? sortByPrice, bool onlyActive) : IRequest<IReadOnlyList<DishResponse>>;
}
