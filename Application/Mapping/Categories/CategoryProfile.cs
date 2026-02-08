using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Categories
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
        }
    }
}
