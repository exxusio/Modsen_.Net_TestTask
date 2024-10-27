using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.Cancel;
using EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.Register;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class EventRegistrationMappingProfile : Profile
    {
        public EventRegistrationMappingProfile()
        {
            CreateMap<RegisterEventRegistrationCommand, EventRegistration>()
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());

            CreateMap<CancelEventRegistrationCommand, EventRegistration>()
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.Participant, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());

            CreateMap<EventRegistration, EventRegistrationReadDto>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));
        }
    }
}