using Application.Features.Orders.Commands;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.request.Items)
                .NotNull()
                .WithMessage("La orden debe contener al menos un plato.")
                .Must(x => x.Any())
                .WithMessage("La orden debe contener al menos un plato.");

            RuleFor(x => x.request.Delivery.Id)
                .ValidOrderDelivery();

            RuleForEach(x => x.request.Items)
                .SetValidator(new OrderItemValidator());
        }
    }
}
