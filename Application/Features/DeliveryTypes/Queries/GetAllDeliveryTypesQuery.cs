using Application.Models.Responses;
using MediatR;

namespace Application.Features.DeliveryTypes.Queries
{
    public record GetAllDeliveryTypesQuery() : IRequest<IReadOnlyList<GenericResponse>>;
}
