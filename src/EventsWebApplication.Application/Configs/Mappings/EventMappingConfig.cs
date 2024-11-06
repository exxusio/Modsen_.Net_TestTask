using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class EventMappingConfig
    : Profile
    {
        public EventMappingConfig()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<Event, EventReadDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.HasAvailableSeats, opt => opt.MapFrom(src => src.EventRegistrations.Count() <= src.MaxParticipants))
                .ForMember(dest => dest.RegisteredCount, opt => opt.MapFrom(src => src.EventRegistrations.Count()));
        }
    }
}