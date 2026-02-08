using Application.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Statuses
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, GenericResponse>();
        }
    }
}
