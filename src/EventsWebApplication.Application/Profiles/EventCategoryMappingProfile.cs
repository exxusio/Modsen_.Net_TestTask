using AutoMapper;
using EventsWebApplication.Application.DTOs.EventCategories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class EventCategoryMappingProfile : Profile
    {
        public EventCategoryMappingProfile()
        {
            CreateMap<EventCategoryCreateDto, EventCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<EventCategoryUpdateDto, EventCategory>()
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<EventCategory, EventCategoryReadDto>();

            CreateMap<EventCategory, EventCategoryDetailedReadDto>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events));
        }
    }
}