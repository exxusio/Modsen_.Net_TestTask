using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class EventCategoryMappingConfig
    : Profile
    {
        public EventCategoryMappingConfig()
        {
            CreateMap<CreateCategoryCommand, EventCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());

            CreateMap<EventCategory, EventCategoryReadDto>();
        }
    }
}