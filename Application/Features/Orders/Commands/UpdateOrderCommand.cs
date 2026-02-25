using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderCommand(long id, OrderUpdateRequest request) : IRequest<OrderUpdateReponse>;
}
