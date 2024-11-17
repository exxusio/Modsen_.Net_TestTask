using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.Abstractions.Notify
{
    public interface INotificationService
    {
        Task SendToAllEventChange(EventReadDto _event, string message, string type, CancellationToken cancellationToken);
    }
}