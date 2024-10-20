using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace DataAccessLayer.Data.Implementations
{
    public class EventRegistrationRepository : BaseRepository<EventRegistration>, IEventRegistrationRepository
    {
        public EventRegistrationRepository(AppDbContext context) : base(context)
        {

        }
    }
}