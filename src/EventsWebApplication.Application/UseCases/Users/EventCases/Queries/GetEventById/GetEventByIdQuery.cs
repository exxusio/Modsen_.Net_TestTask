using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<EventReadDto>
    {
        public Guid EventId { get; set; }
    }
}