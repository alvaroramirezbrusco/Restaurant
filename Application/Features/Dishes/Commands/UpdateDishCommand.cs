using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public record UpdateDishCommand(Guid id, DishUpdateRequest request) : IRequest<DishResponse>;
}
