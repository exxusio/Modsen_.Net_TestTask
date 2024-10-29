using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventCategoryRepository : IRepository<EventCategory>
    {
        Task<EventCategory?> GetCategoryByName(string name, CancellationToken cancellationToken = default);
    }
}