using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IReadOnlyList<OrderDetailsResponse>>
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(
            IOrderQuery orderQuery,
            IMapper mapper)
        {
            _orderQuery = orderQuery;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OrderDetailsResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderQuery.GetAllAsync(request.from, request.to, request.status, cancellationToken);

            return _mapper.Map<IReadOnlyList<OrderDetailsResponse>>(orders);
        }
    }
}
