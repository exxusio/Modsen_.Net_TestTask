using AutoMapper;
using EventsWebApplication.Application.DTOs.EventRegistrations;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Profiles
{
    public class EventRegistrationMappingProfile : Profile
    {
        public EventRegistrationMappingProfile()
        {
            CreateMap<EventRegistrationCreateDto, EventRegistration>()
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore());

            CreateMap<EventRegistrationUpdateDto, EventRegistration>()
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore());

            CreateMap<EventRegistration, EventRegistrationReadDto>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.Participant.FirstName + " " + src.Participant.LastName));

            CreateMap<EventRegistration, EventRegistrationDetailedReadDto>()
                .ForMember(dest => dest.Event.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Participant.Id, opt => opt.MapFrom(src => src.ParticipantId))
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));
        }
    }
}