using Application.Features.Categories.Queries;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.DeliveryTypes.Queries
{
    public class GetAllDeliveryTypesHandler : IRequestHandler<GetAllDeliveryTypesQuery, IReadOnlyList<GenericResponse>>
    {
        private readonly IDeliveryTypeQuery _deliveryTypeQuery;
        private readonly IMapper _mapper;

        public GetAllDeliveryTypesHandler(
            IDeliveryTypeQuery deliveryTypeQuery,
            IMapper mapper)
        {
            _deliveryTypeQuery = deliveryTypeQuery;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GenericResponse>> Handle(GetAllDeliveryTypesQuery request, CancellationToken cancellationToken)
        {
            var deliveryTypes = await _deliveryTypeQuery.GetAllAsync(cancellationToken);

            return _mapper.Map<IReadOnlyList<GenericResponse>>(deliveryTypes);
        }
    }
}
