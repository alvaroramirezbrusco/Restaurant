using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderUpdateReponse>
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderCommand _orderCommand;
        private readonly IDishQuery _dishQuery;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(
            IOrderQuery orderQuery,
            IOrderCommand orderCommand,
            IDishQuery dishQuery,
            IMapper mapper)
        {
            _orderQuery = orderQuery;
            _orderCommand = orderCommand;
            _dishQuery = dishQuery;
            _mapper = mapper;
        }

        public async Task<OrderUpdateReponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQuery.GetByIdAsync(request.id)
                ?? throw new KeyNotFoundException("Orden no encontrada");

            if (order.OverallStatusNavigation.Id == StatusIds.Closed)
            {
                throw new ArgumentException("No se puede modificar una orden que ya está cerrada");
            }

            var dishIds = request.request.Items.Select(item => item.Id).ToList();
            var existingDishes = await _dishQuery.GetDishesByIdAsync(dishIds);
            var existingDishesDict = existingDishes.ToDictionary(d => d.DishId);


            foreach (var item in request.request.Items)
            {
                if (!existingDishesDict.TryGetValue(item.Id, out var dish) || !dish.Available)
                {
                    throw new ArgumentException("El plato especificado no existe o no está disponible");
                }

                var pendingItem = order.OrderItems
                    .FirstOrDefault(oi => oi.Dish == item.Id && oi.Status == StatusIds.Pending);

                if (pendingItem != null)
                {
                    pendingItem.Quantity = item.Quantity;
                    pendingItem.Notes = item.Notes;
                }
                else
                {
                    var newItem = _mapper.Map<OrderItem>(item);
                    newItem.Order = order.OrderId;
                    newItem.CreateDate = DateTime.UtcNow;

                    order.OrderItems.Add(newItem);
                }
            }

            var allDishIds = order.OrderItems.Select(oi => oi.Dish).ToList();
            var allDishes = await _dishQuery.GetDishesByIdAsync(allDishIds);
            var allDishesDict = allDishes.ToDictionary(d => d.DishId);

            order.Price = order.OrderItems
                .Sum(oi => oi.Quantity * allDishesDict[oi.Dish].Price);

            order.UpdateDate = DateTime.UtcNow;

            await _orderCommand.UpdateAsync(order, cancellationToken);

            return _mapper.Map<OrderUpdateReponse>(order);
        }
    }
}
