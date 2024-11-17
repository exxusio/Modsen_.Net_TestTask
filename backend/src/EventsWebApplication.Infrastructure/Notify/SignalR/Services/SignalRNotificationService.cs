using Microsoft.AspNetCore.SignalR;
using EventsWebApplication.Infrastructure.Notify.SignalR.Hubs;
using EventsWebApplication.Application.Abstractions.Notify;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Infrastructure.Notify.SignalR.Services
{
    public class SignalRNotificationService(
        IHubContext<EventNotificationHub> _hubContext,
        IEventRegistrationRepository _registrationRepository
    ) : INotificationService
    {
        public async Task SendToAllEventChange(EventReadDto _event, string message, string type, CancellationToken cancellationToken)
        {
            var registrations = await _registrationRepository.GetByEventIdAsync(_event.Id, cancellationToken);

            foreach (var registration in registrations)
            {
                var userConnectionId = registration.Participant.Id.ToString();

                await _hubContext.Clients.User(userConnectionId).SendAsync(
                    type,
                    new
                    {
                        Event = _event,
                        Message = message
                    },
                    cancellationToken
                );
            }
        }
    }
}