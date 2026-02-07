using Domain.Constants;
using FluentValidation;

namespace Application.Validators.Extensions
{
    public static class DishValidationExtensions
    {
        public static IRuleBuilderOptions<T, Guid> ValidDishId<T>(
            this IRuleBuilder<T, Guid> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("Formato de ID inválido");
        }

        public static IRuleBuilderOptions<T, string> ValidDishName<T>(
            this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("El nombre del plato es obligatorio")
                .MaximumLength(DishConstraints.NameMaxLength)
                .WithMessage($"El nombre del plato no puede superar los {DishConstraints.NameMaxLength} caracteres");
        }

        public static IRuleBuilderOptions<T, decimal> ValidDishPrice<T>(
            this IRuleBuilder<T, decimal> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("El precio del plato es obligatorio")
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a cero");
        }

        public static IRuleBuilderOptions<T, int> ValidDishCategory<T>(
            this IRuleBuilder<T, int> rule)
        {
            return rule
                .NotEmpty()
                .WithMessage("La categoría es obligatoria")
                .GreaterThan(0)
                .WithMessage("La categoría es inválida");
        }
    }
}

