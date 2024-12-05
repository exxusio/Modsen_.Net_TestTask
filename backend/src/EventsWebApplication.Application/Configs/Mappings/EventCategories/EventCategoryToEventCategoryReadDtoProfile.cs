using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.EventCategories
{
    public class EventCategoryToEventCategoryReadDtoProfile
    : Profile
    {
        public EventCategoryToEventCategoryReadDtoProfile()
        {
            CreateMap<EventCategory, EventCategoryReadDto>();

            CreateMap<EventCategoryReadDto, EventCategory>()
                .ForMember(dest => dest.Events, opt => opt.Ignore());
        }
    }
}