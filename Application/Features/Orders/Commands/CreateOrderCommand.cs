using Application.Models.Requests;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record CreateOrderCommand(OrderRequest request) : IRequest<OrderCreateReponse>;
}
