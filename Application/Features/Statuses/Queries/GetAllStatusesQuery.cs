using Application.Models.Responses;
using MediatR;

namespace Application.Features.Statuses.Queries
{
    public record GetAllStatusesQuery() : IRequest<IReadOnlyList<GenericResponse>>;
}
