using Application.Features.Orders.Commands;
using FluentValidation;

namespace Application.Features.Orders.Validators.Commands
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(x => x.request.Status)
                .NotEmpty()
                .WithMessage("El estado especificado no es válido")
                .GreaterThan(0)
                .WithMessage("El estado especificado no es válido");
        }
    }
}
