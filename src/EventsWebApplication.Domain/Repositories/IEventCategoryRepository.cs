using EventsWebApplication.Domain.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Repositories
{
    public interface IEventCategoryRepository
    : IRepository<EventCategory>
    {
        Task<EventCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}