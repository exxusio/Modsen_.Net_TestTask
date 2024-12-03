
using AutoMapper;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.EventRegistrations
{
    public class RegisterForEventCommandToEventRegistrationProfile
    : Profile
    {
        public RegisterForEventCommandToEventRegistrationProfile()
        {
            CreateMap<RegisterForEventCommand, EventRegistration>()
                .ForMember(dest => dest.ParticipantId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}