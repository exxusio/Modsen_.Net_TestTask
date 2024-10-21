using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRegistrationRepository : BaseRepository<EventRegistration>, IEventRegistrationRepository
    {
        public EventRegistrationRepository(AppDbContext context) : base(context)
        {

        }
    }
}