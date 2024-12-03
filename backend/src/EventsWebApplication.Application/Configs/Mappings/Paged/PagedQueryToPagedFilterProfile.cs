using AutoMapper;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Application.Configs.Mappings.Paged
{
    public class PagedQueryToPagedFilterProfile
    : Profile
    {
        public PagedQueryToPagedFilterProfile()
        {
            CreateMap<PagedQuery, PagedFilter>();
        }
    }
}