using Application.Models.Requests;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class OrderItemValidator : AbstractValidator<Items>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .ValidQuantity();
        }
    }
}
