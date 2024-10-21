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

            CreateMap<EventCriteriaDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.MaxParticipants, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<Event, EventReadDto>();

            CreateMap<Event, EventDetailedReadDto>()
                .ForMember(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}