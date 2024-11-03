using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetAllRegistrations
{
    public class GetAllRegistrationsQuery
    : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
    }
}