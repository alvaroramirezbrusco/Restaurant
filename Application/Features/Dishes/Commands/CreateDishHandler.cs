using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, DishResponse>
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;

        public CreateDishHandler(IDishCommand dishCommand, IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }

        public async Task<DishResponse> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Price <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a cero");
            }

            if (string.IsNullOrEmpty(request.request.Name))
            {
                throw new ArgumentException("El nombre del plato es obligatorio");
            }

            if (request.request.Category <= 0)
            {
                throw new ArgumentException("La categoría es inválida");
            }

            var existingCategory = await _categoryQuery.GetByIdAsync(request.request.Category);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("No se encontró la categoría ingresada");
            }

            var existingDish = await _dishQuery.GetByNameAsync(request.request.Name);
            if (existingDish != null)
            {
                throw new ConflictException("Ya existe un plato con ese nombre");
            }

            var dish = new Dish
            {
                Name = request.request.Name,
                Description = request.request.Description,
                Price = request.request.Price,
                Available = true,
                Category = request.request.Category,
                ImageUrl = request.request.Image,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await _dishCommand.InsertAsync(dish);

            return new DishResponse
            {
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = new GenericResponse
                {
                    Id = existingCategory.Id,
                    Name = existingCategory.Name
                },
                IsActive = dish.Available,
                Image = dish.ImageUrl,
                CreatedAt = dish.CreateDate,
                UpdatedAt = dish.UpdateDate
            };
        }
    }
}
