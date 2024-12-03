
using AutoMapper;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.Configs.Mappings.Filters
{
    public class GetEventsByFilterQueryToEventFilterProfile
    : Profile
    {
        public GetEventsByFilterQueryToEventFilterProfile()
        {
            CreateMap<GetEventsByFilterQuery, EventFilter>();
        }
    }
}