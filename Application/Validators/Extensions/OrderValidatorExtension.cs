using FluentValidation;

namespace Application.Validators.Extensions
{
    public static class OrderValidatorExtension
    {
        public static IRuleBuilderOptions<T, int> ValidOrderDelivery<T>(
            this IRuleBuilder<T, int> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("Debe especificar un tipo de entrega válido")
                .GreaterThan(0)
                .WithMessage("Debe especificar un tipo de entrega válido");
        }
    }
}
