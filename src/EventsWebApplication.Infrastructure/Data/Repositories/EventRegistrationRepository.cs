using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Specifications;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRegistrationRepository(
        AppDbContext context)
        : BaseRepository<EventRegistration>(context), IEventRegistrationRepository
    {
        public async Task<EventRegistration?> GetRegistrationByEventIdAndParticipantId(Guid userId, Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationByEventIdAndParticipantIdSpecification(userId, eventId);

            var registration = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetRegistrationsByParticipantId(Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByParticipantIdSpecification(userId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetRegistrationsByEventId(Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByEventIdSpecification(eventId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }
    }
}