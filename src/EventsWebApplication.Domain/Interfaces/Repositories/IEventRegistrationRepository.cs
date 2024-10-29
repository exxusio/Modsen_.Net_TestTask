using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventRegistrationRepository : IRepository<EventRegistration>
    {
        Task<EventRegistration?> GetRegistrationByEventIdAndParticipantIdAsync(Guid eventId, Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetRegistrationsByParticipantIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetRegistrationsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    }
}