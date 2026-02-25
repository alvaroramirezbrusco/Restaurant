using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, DishResponse>
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IMapper _mapper;

        public CreateDishHandler(
            IDishCommand dishCommand,
            IDishQuery dishQuery,
            ICategoryQuery categoryQuery,
            IMapper mapper)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
            _mapper = mapper;
        }

        public async Task<DishResponse> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryQuery.GetByIdAsync(request.request.Category, cancellationToken);
            if (category == null)
            {
                throw new KeyNotFoundException("No se encontró la categoría ingresada");
            }

            var existingDish = await _dishQuery.ExistsWithNameAsync(request.request.Name, cancellationToken);
            if (existingDish != null)
            {
                throw new ConflictException("Ya existe un plato con ese nombre");
            }

            var dish = _mapper.Map<Dish>(request.request);

            await _dishCommand.InsertAsync(dish, cancellationToken);

            dish.CreateDate = DateTime.UtcNow;
            dish.UpdateDate = DateTime.UtcNow;

            return _mapper.Map<DishResponse>(dish);
        }

    }
}
