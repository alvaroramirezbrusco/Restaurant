using Application.Models.Requests;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<OrderItemUpdateRequest>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(x => x.Status)
                .ValidStatus();
        }
    }
}
