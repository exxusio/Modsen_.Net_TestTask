using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class EventRegistrationMappingConfig
    : Profile
    {
        public EventRegistrationMappingConfig()
        {
            CreateMap<RegisterForEventCommand, EventRegistration>()
                .ForMember(dest => dest.ParticipantId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<EventRegistration, EventRegistrationReadDto>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));
        }
    }
}