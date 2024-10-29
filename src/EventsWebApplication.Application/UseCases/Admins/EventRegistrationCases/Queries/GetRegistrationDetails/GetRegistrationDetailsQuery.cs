using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetRegistrationDetails
{
    public class GetRegistrationDetailsQuery : IRequest<EventRegistrationReadDto>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}