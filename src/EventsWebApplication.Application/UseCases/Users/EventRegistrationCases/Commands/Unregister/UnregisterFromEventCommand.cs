using MediatR;
using EventsWebApplication.Application.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Commands.Unregister
{
    public class UnregisterFromEventCommand : IRequest<EventRegistrationReadDto>
    {
        public Guid Id { get; set; }

        [BindNever]
        public Guid UserId { get; set; }
    }
}