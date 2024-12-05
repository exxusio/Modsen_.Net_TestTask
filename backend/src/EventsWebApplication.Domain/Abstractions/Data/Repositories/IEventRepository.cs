using EventsWebApplication.Domain.Abstractions.Data.Repositories.Bases;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Domain.Abstractions.Data.Repositories
{
    public interface IEventRepository
    : IRepository<Event>
    {
        void Track(Event _event);
        void Update(Event _event);
        Task<(IEnumerable<Event>, int)> GetByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken);
        Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}