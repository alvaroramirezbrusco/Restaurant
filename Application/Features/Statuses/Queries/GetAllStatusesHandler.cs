using Application.Features.DeliveryTypes.Queries;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Statuses.Queries
{
    public class GetAllStatusesHandler : IRequestHandler<GetAllStatusesQuery, IReadOnlyList<GenericResponse>>
    {
        private readonly IStatusQuery _statusQuery;
        private readonly IMapper _mapper;

        public GetAllStatusesHandler(
            IStatusQuery statusQuery,
            IMapper mapper)
        {
            _statusQuery = statusQuery;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GenericResponse>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _statusQuery.GetAllAsync(cancellationToken);

            return _mapper.Map<IReadOnlyList<GenericResponse>>(statuses);
        }
    }
}
