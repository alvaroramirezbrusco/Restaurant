using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailsResponse>
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(
            IOrderQuery orderQuery,
            IMapper mapper)
        {
            _orderQuery = orderQuery;
            _mapper = mapper;
        }

        public async Task<OrderDetailsResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderQuery.GetByIdAsync(request.id, cancellationToken);
            if (order == null)
            {
                throw new KeyNotFoundException("Orden no encontrada");
            }
            return _mapper.Map<OrderDetailsResponse>(order);
        }
    }
}
