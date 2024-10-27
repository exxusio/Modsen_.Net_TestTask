using MediatR;
using EventsWebApplication.Application.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.Register
{
    public class RegisterForEventCommand : IRequest<EventRegistrationReadDto>
    {
        public Guid EventId { get; set; }

        [BindNever]
        public Guid UserId { get; set; }
    }
}