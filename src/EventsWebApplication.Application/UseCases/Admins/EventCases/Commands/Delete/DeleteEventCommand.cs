using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.Delete
{
    public class DeleteEventCommand : IRequest<EventReadDto>
    {
        public Guid Id { get; set; }
    }
}