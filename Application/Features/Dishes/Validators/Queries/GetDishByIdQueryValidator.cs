using Application.Features.Dishes.Queries;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Dishes.Validators.Queries
{
    public class GetDishByIdQueryValidator : AbstractValidator<GetDishByIdQuery>
    {
        public GetDishByIdQueryValidator()
        {
            RuleFor(x => x.id)
                .ValidDishId();
        }
    }
}
