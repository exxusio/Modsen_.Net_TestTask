using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.MappingConfigurations
{
    public class FilterMappingProfile : Profile
    {
        public FilterMappingProfile()
        {
            CreateMap<GetEventsByFilterQuery, EventFilter>();

            CreateMap<PagedQuery, PagedFilter>();
        }
    }
}