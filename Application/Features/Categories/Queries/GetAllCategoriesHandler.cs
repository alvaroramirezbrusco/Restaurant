using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryResponse>>
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(
            ICategoryQuery categoryQuery,
            IMapper mapper)
        {
            _categoryQuery = categoryQuery;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryQuery.GetAllAsync(cancellationToken);

            return _mapper.Map<IReadOnlyList<CategoryResponse>>(categories);
        }
    }
}
