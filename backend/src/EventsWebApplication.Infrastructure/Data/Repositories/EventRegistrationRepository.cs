using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Infrastructure.Data.Specifications;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventRegistrationRepository(
        AppDbContext context
    ) : BaseRepository<EventRegistration>(context), IEventRegistrationRepository
    {
        public async Task<EventRegistration?> GetByEventIdAndParticipantIdAsync(Guid eventId, Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new RegistrationByEventIdAndParticipantIdSpecification(eventId, userId);

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