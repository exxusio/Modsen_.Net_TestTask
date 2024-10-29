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
        public async Task<EventRegistration?> GetRegistrationByEventIdAndParticipantIdAsync(Guid userId, Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationByEventIdAndParticipantIdSpecification(userId, eventId);

            var registration = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetRegistrationsByParticipantIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByParticipantIdSpecification(userId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetRegistrationsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByEventIdSpecification(eventId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }
    }
}