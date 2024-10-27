using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAll
{
    public class GetAllEventsQuery : IRequest<IEnumerable<EventReadDto>>
    {
    }
}