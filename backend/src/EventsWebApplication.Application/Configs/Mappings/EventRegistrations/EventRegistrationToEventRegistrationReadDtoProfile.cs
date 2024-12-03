
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.EventRegistrations
{
    public class EventRegistrationToEventRegistrationReadDtoProfile
    : Profile
    {
        public EventRegistrationToEventRegistrationReadDtoProfile()
        {
            CreateMap<EventRegistration, EventRegistrationReadDto>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));
        }
    }
}