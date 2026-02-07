using Application.Models.Requests;
using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Dishes
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Available))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new GenericResponse
                {
                    Id = src.CategoryNavigator.Id,
                    Name = src.CategoryNavigator.Name
                }));

            CreateMap<DishRequest, Dish>()
                .ForMember(dest => dest.DishId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Available, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<DishUpdateRequest, Dish>()
                .ForMember(dest => dest.DishId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
