using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<EventReadDto>
    {
        public Guid Id { get; set; }
    }
}