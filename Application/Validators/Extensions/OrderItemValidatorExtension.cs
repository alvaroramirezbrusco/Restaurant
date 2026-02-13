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
    }
}
