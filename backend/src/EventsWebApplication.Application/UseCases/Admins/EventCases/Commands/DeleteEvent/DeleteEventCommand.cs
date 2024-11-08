using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent
{
    public class DeleteEventCommand
    : IRequest<EventReadDto>
    {
        [BindNever]
        [JsonIgnore]
        public Guid EventId { get; set; }
    }
}