using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderCreateReponse>
    {
        private readonly IOrderCommand _orderCommand;
        private readonly IDeliveryTypeQuery _deliveryTypeQuery;
        private readonly IDishQuery _dishQuery;
        private readonly IMapper _mapper;

        public CreateOrderHandler(
            IOrderCommand orderCommand,
            IDeliveryTypeQuery deliveryTypeQuery,
            IDishQuery dishQuery,
            IMapper mapper)
        {
            _orderCommand = orderCommand;
            _deliveryTypeQuery = deliveryTypeQuery;
            _dishQuery = dishQuery;
            _mapper = mapper;
        }

        public async Task<OrderCreateReponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingDelivery = await _deliveryTypeQuery.GetByIdAsync(request.request.Delivery.Id, cancellationToken);
            if (existingDelivery == null)
            {
                throw new KeyNotFoundException("Tipo de entrega no encontrada");
            }
            var dishIds = request.request.Items.Select(item => item.Id).ToList();
            var existingDishes = await _dishQuery.GetDishesByIdAsync(dishIds);
            var existingDishesDict = existingDishes.ToDictionary(d => d.DishId);

            decimal totalAmount = 0m;

            foreach (var item in request.request.Items)
            {
                if (!existingDishesDict.TryGetValue(item.Id, out var dish) || !dish.Available)
                {
                    throw new ArgumentException("El plato especificado no existe o no está disponible");
                }
                totalAmount += dish.Price * item.Quantity;
            }

            var order = _mapper.Map<Order>(request.request);

            order.Price = totalAmount;
            order.CreateDate = DateTime.UtcNow;
            order.UpdateDate = DateTime.UtcNow;

            foreach (var item in order.OrderItems)
            {
                item.CreateDate = DateTime.UtcNow;
            }

            await _orderCommand.InsertAsync(order, cancellationToken);

            return _mapper.Map<OrderCreateReponse>(order);
        }
    }
}
