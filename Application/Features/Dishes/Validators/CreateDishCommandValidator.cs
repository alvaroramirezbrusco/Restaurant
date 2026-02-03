using Application.Features.Dishes.Commands;
using Application.Interfaces.Query;
using Application.Validators.Bases;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Dishes.Validators
{
    public class CreateDishCommandValidator
        : DishValidatorBase<CreateDishCommand>
    {
        public CreateDishCommandValidator(ICategoryQuery categoryQuery, IDishQuery dishQuery)
        {
            RuleFor(x => x.request.Name)
                .Cascade(CascadeMode.Stop)
                .ValidDishName();

            AddDishNameUniquenessRule(
                RuleFor(x => x.request.Name),
                dishQuery);

            RuleFor(x => x.request.Price)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a cero");

            RuleFor(x => x.request.Category)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage("La categoría es inválida");

            AddCategoryExistsRule(
                RuleFor(x => x.request.Category),
                categoryQuery);
        }
    }
}
