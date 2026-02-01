using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Queries
{
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, DishResponse>
    {
        private readonly IDishQuery _dishQuery;

        public GetDishByIdHandler(IDishQuery dishQuery)
        {
            _dishQuery = dishQuery;
        }

        public async Task<DishResponse> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
            {
                throw new ArgumentException("Formato de ID inválido");
            }

            var dish = await _dishQuery.GetByIdAsync(request.id);
            if (dish == null)
            {
                throw new KeyNotFoundException("Plato no encontrado");
            }

            return new DishResponse
            {
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = new GenericResponse
                {
                    Id = dish.CategoryNavigator.Id,
                    Name = dish.CategoryNavigator.Name
                },
                IsActive = dish.Available,
                Image = dish.ImageUrl,
                CreatedAt = dish.CreateDate,
                UpdatedAt = dish.UpdateDate
            };
        }
    }
}
