using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Queries
{
    public record GetDishByIdQuery(Guid id) : IRequest<DishResponse>;
}
