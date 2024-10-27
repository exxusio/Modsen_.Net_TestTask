using MediatR;
using EventsWebApplication.Application.DTOs.Events;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetEventsByParticipantId
{
    public class GetEventsByParticipantIdQuery : IRequest<IEnumerable<EventReadDto>>
    {
        [BindNever]
        public Guid UserId { get; set; }
    }
}