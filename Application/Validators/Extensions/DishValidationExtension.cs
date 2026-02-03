using Domain.Constants;
using FluentValidation;

namespace Application.Validators.Extensions
{
    public static class DishValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidDishName<T>(
            this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("El nombre del plato es obligatorio")
                .MaximumLength(DishConstraints.NameMaxLength)
                .WithMessage($"El nombre del plato no puede superar los {DishConstraints.NameMaxLength} caracteres");
        }
    }
}

