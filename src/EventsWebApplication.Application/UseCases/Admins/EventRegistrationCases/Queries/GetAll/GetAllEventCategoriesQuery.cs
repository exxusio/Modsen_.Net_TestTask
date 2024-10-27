using MediatR;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetAll
{
    public class GetAllEventRegistrationsQuery : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
    }
}