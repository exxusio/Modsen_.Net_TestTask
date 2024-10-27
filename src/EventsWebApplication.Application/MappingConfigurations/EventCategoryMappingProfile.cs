using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.Create;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class EventCategoryMappingProfile : Profile
    {
        public EventCategoryMappingProfile()
        {
            CreateMap<CreateEventCategoryCommand, EventCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<EventCategory, EventCategoryReadDto>();
        }
    }
}