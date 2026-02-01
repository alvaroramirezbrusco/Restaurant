using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Dishes.Commands
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand, DishResponse>
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;

        public UpdateDishHandler(IDishCommand dishCommand, IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }

        public async Task<DishResponse> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var existingDish = await _dishQuery.GetByIdAsync(request.id);

            if (existingDish == null)
            {
                throw new KeyNotFoundException("Plato no encontrado");
            }

            if (request.request.Price <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a cero");
            }

            if (string.IsNullOrEmpty(request.request.Name))
            {
                throw new ArgumentException("El nombre del plato es obligatorio");
            }
            var existingCategory = await _categoryQuery.GetByIdAsync(request.request.Category);
            if (existingCategory == null)
            {
                throw new ArgumentException("La categoría es inválida");
            }
            var dish = await _dishQuery.GetByNameAsync(request.request.Name);
            if (dish != null && dish.DishId != existingDish.DishId)
            {
                throw new ConflictException("Ya existe un plato con ese nombre");
            }

            existingDish.Name = request.request.Name;
            existingDish.Description = request.request.Description;
            existingDish.Price = request.request.Price;
            existingDish.Category = request.request.Category;
            existingDish.ImageUrl = request.request.Image;
            existingDish.Available = request.request.IsActive;
            existingDish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateAsync(existingDish);

            return new DishResponse
            {
                Id = existingDish.DishId,
                Name = existingDish.Name,
                Description = existingDish.Description,
                Price = existingDish.Price,
                Category = new GenericResponse
                {
                    Id = existingCategory.Id,
                    Name = existingCategory.Name
                },
                IsActive = existingDish.Available,
                Image = existingDish.ImageUrl,
                CreatedAt = existingDish.CreateDate,
                UpdatedAt = existingDish.UpdateDate
            };
        }
    }
}
