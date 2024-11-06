using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.UnregisterFromEvent
{
    public class UnregisterFromEventCommand
    : IRequest<EventRegistrationReadDto>
    {
        [BindNever]
        public Guid RegistrationId { get; set; }
        [BindNever]
        public Guid UserId { get; set; }
    }
}