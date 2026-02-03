using Application.Interfaces.Query;
using FluentValidation;

namespace Application.Validators.Bases
{
    public abstract class DishValidatorBase<T> : AbstractValidator<T>
    {
        protected void AddDishNameUniquenessRule(
            IRuleBuilder<T, string> rule,
            IDishQuery dishQuery)
        {
            rule
                .MustAsync(async (name, _) =>
                    await dishQuery.GetByNameAsync(name) == null)
                .WithMessage("Ya existe un plato con ese nombre");
        }

        protected void AddCategoryExistsRule(
            IRuleBuilder<T, int> rule,
            ICategoryQuery categoryQuery)
        {
            rule
                .MustAsync(async (id, _) =>
                    await categoryQuery.GetByIdAsync(id) != null)
                .WithMessage("No se encontró la categoría ingresada");
        }
    }
}
