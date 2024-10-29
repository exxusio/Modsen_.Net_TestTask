using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAllEvents
{
    public class GetAllEventsQuery : IRequest<IEnumerable<EventReadDto>>
    {
    }
}