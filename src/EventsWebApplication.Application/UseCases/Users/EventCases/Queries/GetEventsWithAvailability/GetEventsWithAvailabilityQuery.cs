using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsWithAvailability
{
    public class GetEventsWithAvailabilityQuery : IRequest<IEnumerable<EventWithAvailabilityDto>>
    {
    }
}