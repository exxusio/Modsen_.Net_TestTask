using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.Configs.Mappings
{
    public class PagedMappingConfig
    : Profile
    {
        public PagedMappingConfig()
        {
            CreateMap<PagedQuery, PagedFilter>();
        }
    }
}