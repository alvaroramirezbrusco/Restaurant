using Application.Models.Responses;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetAllOrdersQuery(DateTime? from, DateTime? to, int? status) : IRequest<IReadOnlyList<OrderDetailsResponse>>;
}
