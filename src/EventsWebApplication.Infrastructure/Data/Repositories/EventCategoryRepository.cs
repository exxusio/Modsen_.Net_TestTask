using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventCategoryRepository : BaseRepository<EventCategory>, IEventCategoryRepository
    {
        public EventCategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}