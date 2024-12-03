using EventsWebApplication.Domain.Abstractions.Data.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Abstractions.Data.Repositories
{
    public interface IEventCategoryRepository
    : IRepository<EventCategory>
    {
        Task<EventCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}