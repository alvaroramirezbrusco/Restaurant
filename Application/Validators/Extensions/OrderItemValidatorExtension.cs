using FluentValidation;

namespace Application.Validators.Extensions
{
    public static class OrderItemValidatorExtension
    {
        public static IRuleBuilderOptions<T, int> ValidQuantity<T>(
            this IRuleBuilder<T, int> rule)
        {
            return rule
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0");
        }

        public static IRuleBuilderOptions<T, int> ValidStatus<T>(
            this IRuleBuilder<T, int> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("El estado especificado no es válido")
                .GreaterThan(0)
                .WithMessage("El estado especificado no es válido");
        }
    }
}
