using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAllEvents
{
    public class GetAllEventsQuery : IRequest<IEnumerable<EventReadDto>>
    {
    }
}