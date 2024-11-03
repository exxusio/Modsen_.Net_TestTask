using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class FilterMappingConfig
    : Profile
    {
        public FilterMappingConfig()
        {
            CreateMap<GetEventsByFilterQuery, EventFilter>();

            CreateMap<PagedQuery, PagedFilter>();
        }
    }
}