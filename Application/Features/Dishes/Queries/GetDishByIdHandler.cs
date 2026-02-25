using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Dishes.Queries
{
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, DishResponse>
    {
        private readonly IDishQuery _dishQuery;
        private readonly IMapper _mapper;

        public GetDishByIdHandler(
            IDishQuery dishQuery,
            IMapper mapper)
        {
            _dishQuery = dishQuery;
            _mapper = mapper;
        }

        public async Task<DishResponse> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishQuery.GetByIdAsync(request.id, cancellationToken);
            if (dish == null)
            {
                throw new KeyNotFoundException("Plato no encontrado");
            }

            return _mapper.Map<DishResponse>(dish);
        }
    }
}
