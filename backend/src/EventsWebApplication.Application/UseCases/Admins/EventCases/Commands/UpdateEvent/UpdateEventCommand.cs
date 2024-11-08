using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent
{
    public class UpdateEventCommand
    : IRequest<EventReadDto>
    {
        [BindNever]
        [JsonIgnore]
        public Guid EventId { get; set; }
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