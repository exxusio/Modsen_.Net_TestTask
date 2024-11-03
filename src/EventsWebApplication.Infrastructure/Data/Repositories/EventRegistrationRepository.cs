using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRegistrationRepository(
        AppDbContext context
    ) : BaseRepository<EventRegistration>(context), IEventRegistrationRepository
    {
        public async Task<EventRegistration?> GetByEventIdAndParticipantIdAsync(Guid userId, Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationByEventIdAndParticipantIdSpecification(userId, eventId);

            var registration = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetByParticipantIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByParticipantIdSpecification(userId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }

        public async Task<IEnumerable<EventRegistration>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationsByEventIdSpecification(eventId);

            var registration = await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken);

            return registration;
        }
    }
}