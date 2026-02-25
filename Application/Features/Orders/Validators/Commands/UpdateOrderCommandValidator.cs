using Application.Features.Orders.Commands;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleForEach(x => x.request.Items)
                .SetValidator(new OrderItemValidator());
        }
    }
}
