using FluentValidation;

namespace Application.Features.Dishes.Queries
{
    public class GetAllDishesQueryValidator : AbstractValidator<GetAllDishesQuery>
    {
        public GetAllDishesQueryValidator()
        {
            RuleFor(x => x.categoryId)
                .Must(id => !id.HasValue || id.Value > 0)
                .WithMessage("Parámetros de ordenamiento inválidos");
        }
    }
}
