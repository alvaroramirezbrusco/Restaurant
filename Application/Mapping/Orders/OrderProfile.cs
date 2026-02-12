using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDetailsResponse>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new GenericResponse
                {
                    Id = src.OverallStatusNavigation.Id,
                    Name = src.OverallStatusNavigation.Name
                }))
                .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => new GenericResponse
                {
                    Id = src.DeliveryTypeNavigator.Id,
                    Name = src.DeliveryTypeNavigator.Name
                }))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems.Select(item => new OrderItemResponse
                {
                    Id = item.OrderItemId,
                    Quantity = item.Quantity,
                    Notes = item.Notes,
                    Status = new GenericResponse
                    {
                        Id = item.StatusNavigator.Id,
                        Name = item.StatusNavigator.Name
                    },
                    Dish = new DishShortResponse
                    {
                        Id = item.DishNavigator.DishId,
                        Name = item.DishNavigator.Name,
                        Image = item.DishNavigator.ImageUrl
                    }
                }).ToList()));
        }
    }
}
