using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Events
{
    public class EventToEventReadDtoProfile
    : Profile
    {
        public EventToEventReadDtoProfile()
        {
            CreateMap<Event, EventReadDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.HasAvailableSeats, opt =>
                    opt.MapFrom(src =>
                        (src.EventRegistrations != null
                            ? src.EventRegistrations.Count()
                            : 0) < src.MaxParticipants
                    )
                )
                .ForMember(dest => dest.RegisteredCount, opt =>
                    opt.MapFrom(src =>
                        src.EventRegistrations != null
                            ? src.EventRegistrations.Count()
                            : 0
                    )
                );

            CreateMap<EventReadDto, Event>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());
        }
    }
}