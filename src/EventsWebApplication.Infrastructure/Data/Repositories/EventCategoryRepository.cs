using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace DataAccessLayer.Data.Implementations
{
    public class EventCategoryRepository : BaseRepository<EventCategory>, IEventCategoryRepository
    {
        public EventCategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}