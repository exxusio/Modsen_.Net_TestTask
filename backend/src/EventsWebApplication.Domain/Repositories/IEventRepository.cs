using EventsWebApplication.Domain.Repositories.Bases;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Domain.Repositories
{
    public interface IEventRepository
    : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken);
        Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}