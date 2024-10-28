using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IEventRegistrationRepository : IRepository<EventRegistration>
    {
        Task<EventRegistration?> GetRegistrationByEventIdAndParticipantId(Guid eventId, Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetRegistrationsByParticipantId(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistration>> GetRegistrationsByEventId(Guid eventId, CancellationToken cancellationToken = default);
    }
}