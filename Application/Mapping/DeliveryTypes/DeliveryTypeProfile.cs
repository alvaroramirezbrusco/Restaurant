using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.DeliveryTypes
{
    public class DeliveryTypeProfile : Profile
    {
        public DeliveryTypeProfile()
        {
            CreateMap<DeliveryType, GenericResponse>();
        }
    }
}
