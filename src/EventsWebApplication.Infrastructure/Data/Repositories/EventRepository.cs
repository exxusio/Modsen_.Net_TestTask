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
        public async Task<IEnumerable<Event>> GetEventsByFilter(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken)
        {
            // var predicate = PredicateBuilder.True<Event>();

            // if (request.Date.HasValue)
            // {
            //     predicate = predicate.And(_event => _event.Date == request.Date.Value);
            // }

            // if (!string.IsNullOrEmpty(request.Location))
            // {
            //     predicate = predicate.And(_event => _event.Location.Contains(request.Location));
            // }

            // if (request.CategoryId != null)
            // {
            //     predicate = predicate.And(_event => _event.CategoryId == request.CategoryId);
            // }

            // return await _dbSet
            // .Where(predicate)
            // .Paged(paged)
            // .ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetEventByName(string name, CancellationToken cancellationToken = default)
        {
            var specification = new EventByNameSpecification(name);

            var _event = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return _event;
        }
    }
}