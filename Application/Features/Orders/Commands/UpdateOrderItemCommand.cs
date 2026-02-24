using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderItemCommand(long id, long itemId, OrderItemUpdateRequest request) : IRequest<OrderUpdateReponse>;
}
