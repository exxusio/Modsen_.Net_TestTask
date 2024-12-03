using AutoMapper;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Events
{
    public class UpdateEventCommandToEventProfile
    : Profile
    {
        public UpdateEventCommandToEventProfile()
        {
            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());
        }
    }
}