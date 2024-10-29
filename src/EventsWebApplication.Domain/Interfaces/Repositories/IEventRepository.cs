using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsByFilter(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken);
        Task<Event?> GetEventByName(string name, CancellationToken cancellationToken = default);
    }
}