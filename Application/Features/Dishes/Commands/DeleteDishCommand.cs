using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public record DeleteDishCommand(Guid id) : IRequest<DishResponse>;
}
