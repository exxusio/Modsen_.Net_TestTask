using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken);
        Task<Event?> GetEventByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}