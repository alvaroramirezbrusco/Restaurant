using Application.Features.Dishes.Commands;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Dishes.Validators.Commands
{
    public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator()
        {
            RuleFor(x => x.request.Name)
                .Cascade(CascadeMode.Stop)
                .ValidDishName();

            RuleFor(x => x.request.Price)
                .ValidDishPrice();

            RuleFor(x => x.request.Category)
                .Cascade(CascadeMode.Stop)
                .ValidDishCategory();
        }
    }
}
