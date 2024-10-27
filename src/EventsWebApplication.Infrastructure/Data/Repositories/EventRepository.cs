using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRepository(
        AppDbContext context)
        : BaseRepository<Event>(context), IEventRepository
    {
    }
}