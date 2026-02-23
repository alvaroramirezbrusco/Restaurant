using Application.Features.Orders.Queries;
using Application.Validators.Extensions;
using FluentValidation;

namespace Application.Features.Orders.Validators.Queries
{
    public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
    {
        public GetAllOrdersQueryValidator()
        {
            RuleFor(x => x.from)
                .LessThanOrEqualTo(x => x.to.Value)
                .When(x => x.from.HasValue && x.to.HasValue)
                .WithMessage("Rango de fechas inválido");
        }
    }
}
