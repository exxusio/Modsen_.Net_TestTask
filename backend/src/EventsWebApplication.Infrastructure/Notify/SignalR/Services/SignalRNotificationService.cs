using Microsoft.AspNetCore.SignalR;
using EventsWebApplication.Infrastructure.Notify.SignalR.Hubs;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Notify;

namespace EventsWebApplication.Infrastructure.Notify.SignalR.Services
{
    public class SignalRNotificationService(
        IHubContext<EventNotificationHub> _hubContext,
        IEventRegistrationRepository _registrationRepository
    ) : INotificationService
    {
        public async Task SendToAllEventChange(Guid eventId, string eventName, string message, string type, CancellationToken cancellationToken)
        {
            var registrations = await _registrationRepository.GetByEventIdAsync(eventId, cancellationToken);

            foreach (var registration in registrations)
            {
                var userConnectionId = registration.Participant.Id.ToString();

                await _hubContext.Clients.User(userConnectionId).SendAsync(
                    type,
                    new
                    {
                        EventId = eventId.ToString(),
                        EventName = eventName,
                        Message = message
                    },
                    cancellationToken
                );
            }
        }
    }
}