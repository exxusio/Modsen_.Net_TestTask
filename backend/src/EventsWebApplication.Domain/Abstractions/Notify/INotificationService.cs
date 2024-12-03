namespace EventsWebApplication.Domain.Abstractions.Notify
{
    public interface INotificationService
    {
        Task SendToAllEventChange(Guid eventId, string eventName, string message, string type, CancellationToken cancellationToken);
    }
}