using Application.Models.Requests;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<OrderRequest>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Items)
                .NotNull()
                .WithMessage("La orden debe contener al menos un plato.")
                .Must(x => x.Any())
                .WithMessage("La orden debe contener al menos un plato.");

            RuleFor(x => x.Delivery.Id)
                .ValidOrderDelivery();

            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemValidator());
        }
    }
}
