using Application.Exceptions;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Dishes.Commands
{
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand, DishResponse>
    {
        private readonly IDishQuery _dishQuery;
        private readonly IDishCommand _dishCommand;
        private readonly IOrderQuery _orderQuery;
        private readonly IMapper _mapper;

        public DeleteDishHandler(
            IDishQuery dishQuery,
            IDishCommand dishCommand,
            IOrderQuery orderQuery,
            IMapper mapper)
        {
            _dishQuery = dishQuery;
            _dishCommand = dishCommand;
            _orderQuery = orderQuery;
            _mapper = mapper;
        }

        public async Task<DishResponse> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dishQuery.GetByIdAsync(request.id, cancellationToken)
                ?? throw new KeyNotFoundException("Plato no encontrado");

            bool isInActiveOrder = await _orderQuery.IsDishInActiveOrder(request.id, cancellationToken);
            if (isInActiveOrder)
            {
                throw new ConflictException("No se puede eliminar el plato porque está incluido en órdenes activas");
            }

            await _dishCommand.DeleteAsync(dish);

            return _mapper.Map<DishResponse>(dish);
        }
    }
}
