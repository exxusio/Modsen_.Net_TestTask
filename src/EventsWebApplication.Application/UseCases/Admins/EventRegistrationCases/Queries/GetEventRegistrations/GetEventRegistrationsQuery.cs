using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetEventRegistrations
{
    public class GetEventRegistrationsQuery : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
        public Guid EventId { get; set; }
    }
}