using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<EventReadDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public int MaxParticipants { get; set; }
        public Guid CategoryId { get; set; }
    }
}