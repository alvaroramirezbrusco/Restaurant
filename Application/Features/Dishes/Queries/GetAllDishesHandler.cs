using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Dishes.Queries
{
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, IReadOnlyList<DishResponse>>
    {
        private readonly IDishQuery _query;
        private readonly IMapper _mapper;

        public GetAllDishesHandler(
            IDishQuery query,
            IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DishResponse>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _query.GetAllAsync(request.name, request.categoryId, request.sortByPrice, request.onlyActive, cancellationToken);

            return _mapper.Map<IReadOnlyList<DishResponse>>(dishes);
        }
    }
}
