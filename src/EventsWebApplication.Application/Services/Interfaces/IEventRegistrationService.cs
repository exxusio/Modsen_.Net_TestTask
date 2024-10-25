using EventsWebApplication.Application.DTOs.EventRegistrations;

namespace EventsWebApplication.Application.Services.Interfaces
{
    public interface IEventRegistrationService
    {
        Task<IEnumerable<EventRegistrationReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EventRegistrationReadDto> RegisterUserAsync(EventRegistrationCreateDto dto, CancellationToken cancellationToken = default);
        Task<EventRegistrationReadDto> CancelRegistrationAsync(EventRegistrationCancelDto dto, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventRegistrationReadDto>> GetUserRegistrationsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<EventRegistrationDetailedReadDto> GetRegistrationInfoAsync(EventRegistrationInfoDto dto, CancellationToken cancellationToken = default);
        Task NotifyParticipantsAsync(Guid eventId, string message, CancellationToken cancellationToken = default);
    }
}