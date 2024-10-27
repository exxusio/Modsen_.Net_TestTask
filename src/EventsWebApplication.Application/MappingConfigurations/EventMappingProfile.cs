using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.Create;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());

            CreateMap<UpdateEventCommand, Event>()
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