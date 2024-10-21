using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<EventCreateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<EventUpdateDto, Event>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<Event, EventWithAvailabilityDto>()
                .ForMember(dest => dest.HasAvailableSeats, opt => opt.MapFrom(src => src.EventRegistrations.Count < src.MaxParticipants));

            CreateMap<Event, EventReadDto>();

            CreateMap<Event, EventDetailedReadDto>()
                .ForMember(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}