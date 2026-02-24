using Application.Models.Requests;
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

            CreateMap<Order, OrderCreateReponse>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateDate));

            CreateMap<Order, OrderUpdateReponse>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.UpdateDate));

            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.OverallStatus, opt => opt.MapFrom(_ => 1))
                .ForMember(dest => dest.OverallStatusNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.DeliveryTypeNavigator, opt => opt.Ignore())

                .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => src.Delivery.Id))
                .ForMember(dest => dest.DeliveryTo, opt => opt.MapFrom(src => src.Delivery.To))
                
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<Items, OrderItem>()
                .ForMember(dest => dest.OrderItemId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderNavigator, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.DishNavigator, opt => opt.Ignore())
                .ForMember(dest => dest.StatusNavigator, opt => opt.Ignore())

                .ForMember(dest => dest.Dish, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))

                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))

                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => 1));
        }
    }
}
