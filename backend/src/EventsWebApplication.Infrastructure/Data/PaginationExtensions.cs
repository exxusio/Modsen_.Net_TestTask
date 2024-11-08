using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Infrastructure.Data
{
    public static class PaginationExtensions
    {
        public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> query, PagedFilter paged)
        {
            return query
                .Skip((paged.PageNumber - 1) * paged.PageSize)
                .Take(paged.PageSize);
        }
    }
}