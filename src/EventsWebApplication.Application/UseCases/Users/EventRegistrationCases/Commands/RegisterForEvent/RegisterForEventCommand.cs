using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.RegisterForEvent
{
    public class RegisterForEventCommand
    : IRequest<EventRegistrationReadDto>
    {
        [BindNever]
        public Guid EventId { get; set; }
        [BindNever]
        public Guid UserId { get; set; }
    }
}