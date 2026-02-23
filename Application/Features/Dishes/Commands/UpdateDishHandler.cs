using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand, DishResponse>
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IMapper _mapper;

        public UpdateDishHandler(
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

            _mapper.Map(request.request, existingDish);

            existingDish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateAsync(existingDish, cancellationToken);

            return _mapper.Map<DishResponse>(existingDish);
        }
    }
}
