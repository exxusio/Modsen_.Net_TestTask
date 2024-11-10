using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRepository(
        AppDbContext context
    ) : BaseRepository<Event>(context), IEventRepository
    {
        public async Task<(IEnumerable<Event>, int)> GetByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken)
        {
            var specification = new EventsByFilterSpecification(filter);

            var eventsQuery = _dbSet.Where(specification.ToExpression());

            var totalCount = await eventsQuery.CountAsync(cancellationToken);

            var events = await eventsQuery
                .Paged(paged)
                .ToListAsync(cancellationToken);

            return (events, totalCount);
        }

        public async Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var specification = new EventByNameSpecification(name);

            var _event = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return _event;
        }
    }
}