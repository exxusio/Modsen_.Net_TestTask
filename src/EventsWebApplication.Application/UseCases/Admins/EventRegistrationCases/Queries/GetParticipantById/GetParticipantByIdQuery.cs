using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetParticipantById
{
    public class GetParticipantByIdQuery : IRequest<UserReadDto>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}