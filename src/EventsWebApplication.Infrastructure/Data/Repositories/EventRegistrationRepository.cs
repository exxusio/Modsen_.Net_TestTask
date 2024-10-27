using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRegistrationRepository(
        AppDbContext context)
        : BaseRepository<EventRegistration>(context), IEventRegistrationRepository
    {
    }
}