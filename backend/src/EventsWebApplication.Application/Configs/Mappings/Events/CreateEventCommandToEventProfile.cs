using AutoMapper;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.Events
{
    public class CreateEventCommandToEventProfile
    : Profile
    {
        public CreateEventCommandToEventProfile()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.EventRegistrations, opt => opt.Ignore());
        }
    }
}