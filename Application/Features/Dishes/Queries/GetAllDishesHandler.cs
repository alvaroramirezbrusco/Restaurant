using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Dishes.Queries
{
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, IReadOnlyList<DishResponse>>
    {
        private readonly IDishQuery _query;

        public GetAllDishesHandler(IDishQuery query)
        {
            _query = query;
        }

        public async Task<IReadOnlyList<DishResponse>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            if (request.categoryId.HasValue && request.categoryId.Value <= 0)
            {
                throw new ArgumentException("Parámetros de ordenamiento inválidos");
            }

            var dishes = await _query.GetAllAsync(request.name, request.categoryId, request.sortByPrice, request.onlyActive, cancellationToken);
            
            var response = dishes.Select(d => new DishResponse
            {
                Id = d.DishId,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Category = new GenericResponse
                {
                    Id = d.CategoryNavigator.Id,
                    Name = d.CategoryNavigator.Name
                },
                IsActive = d.Available,
                Image = d.ImageUrl,
                CreatedAt = d.CreateDate,
                UpdatedAt = d.UpdateDate
            }).ToList();

            return response;
        }
    }
}
