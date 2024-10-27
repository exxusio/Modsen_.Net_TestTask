using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetParticipantsByEventId
{
    public class GetParticipantsByEventIdQuery : IRequest<IEnumerable<UserReadDto>>
    {
        public Guid EventId { get; set; }
    }
}