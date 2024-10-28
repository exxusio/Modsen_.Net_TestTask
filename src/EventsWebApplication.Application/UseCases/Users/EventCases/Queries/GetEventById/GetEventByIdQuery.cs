using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<EventDetailedReadDto>
    {
        public Guid EventId { get; set; }
    }
}