using Application.Models.Responses;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetOrderByIdQuery(long id) : IRequest<OrderDetailsResponse>
}
