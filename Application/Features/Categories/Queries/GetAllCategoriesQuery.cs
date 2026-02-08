using Application.Models.Responses;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public record GetAllCategoriesQuery() : IRequest<IReadOnlyList<CategoryResponse>>;
}
