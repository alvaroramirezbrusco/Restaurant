using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand, DishResponse>
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;

        public UpdateDishHandler(
            IDishCommand dishCommand,
            IDishQuery dishQuery,
            ICategoryQuery categoryQuery)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }

        public async Task<DishResponse> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var existingDish = await _dishQuery.GetByIdAsync(request.id, cancellationToken)
                ?? throw new KeyNotFoundException("Plato no encontrado");

            var existingCategory = await _categoryQuery.GetByIdAsync(request.request.Category, cancellationToken)
                ?? throw new KeyNotFoundException("No se encontró la categoría ingresada");

            var dishWithSameName = await _dishQuery.ExistsOtherWithNameAsync(request.request.Name, request.id, cancellationToken);
            if (dishWithSameName)
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

            await _dishCommand.UpdateAsync(existingDish, cancellationToken);

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
