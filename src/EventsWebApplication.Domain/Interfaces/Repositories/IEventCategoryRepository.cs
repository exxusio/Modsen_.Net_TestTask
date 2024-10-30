using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventCategoryRepository : IRepository<EventCategory>
    {
        Task<EventCategory?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}