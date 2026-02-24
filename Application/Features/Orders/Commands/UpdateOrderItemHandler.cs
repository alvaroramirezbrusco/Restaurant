using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Models.Responses;
using AutoMapper;
using Domain.Constants;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public class UpdateOrderItemHandler : IRequestHandler<UpdateOrderItemCommand, OrderUpdateReponse>
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderCommand _orderCommand;
        private readonly IStatusQuery _statusQuery;
        private readonly IMapper _mapper;

        public UpdateOrderItemHandler(
            IOrderQuery orderQuery,
            IOrderCommand orderCommand,
            IStatusQuery statusQuery,
            IMapper mapper)
        {
            _orderQuery = orderQuery;
            _orderCommand = orderCommand;
            _statusQuery = statusQuery;
            _mapper = mapper;
        }

        public async Task<OrderUpdateReponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderQuery.GetByIdAsync(request.id, cancellationToken)
                ?? throw new KeyNotFoundException("Orden no encontrada");

            var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == request.itemId)
                ?? throw new KeyNotFoundException("Item no encontrado en la orden");
            
            var status = await _statusQuery.GetByIdAsync(request.request.Status, cancellationToken)
                ?? throw new KeyNotFoundException("Estado no encontrado");
            
            var currentStatus = item.Status;
            if (currentStatus == StatusIds.Delivery && request.request.Status == StatusIds.InProgress)
            {
                throw new ArgumentException("No se puede cambiar de 'Entregado' a 'En preparación'");
            }

            item.Status = request.request.Status;

            var allItemsHaveSameStatus = order.OrderItems.All(i => i.Status == order.OrderItems.First().Status);
            if (allItemsHaveSameStatus)
            {
                order.OverallStatus = order.OrderItems.First().Status;
            }

            order.UpdateDate = DateTime.UtcNow;

            await _orderCommand.UpdateOrder(order);

            return _mapper.Map<OrderUpdateReponse>(order);
        }
    }
}
