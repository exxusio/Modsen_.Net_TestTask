using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Specifications;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRepository(
        AppDbContext context)
        : BaseRepository<Event>(context), IEventRepository
    {
        public async Task<IEnumerable<Event>> GetEventsByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken)
        {
            var specification = new EventsByFilterSpecification(filter);

            return await _dbSet
            .Where(specification.ToExpression())
            .Paged(paged)
            .ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetEventByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var specification = new EventByNameSpecification(name);

            var _event = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return _event;
        }
    }
}