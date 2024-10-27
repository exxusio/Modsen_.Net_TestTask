using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventCategoryRepository(
        AppDbContext context)
        : BaseRepository<EventCategory>(context), IEventCategoryRepository
    {
    }
}