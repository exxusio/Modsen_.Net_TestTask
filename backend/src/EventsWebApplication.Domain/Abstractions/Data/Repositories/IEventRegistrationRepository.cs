using EventsWebApplication.Domain.Abstractions.Data.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Abstractions.Data.Repositories
{
    public interface IEventRegistrationRepository
    : IRepository<EventRegistration>
    {
        Task<EventRegistration?> GetByEventIdAndParticipantIdAsync(Guid eventId, Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetByParticipantIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    }
}