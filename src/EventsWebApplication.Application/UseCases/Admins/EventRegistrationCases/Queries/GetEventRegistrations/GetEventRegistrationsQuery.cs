using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetEventRegistrations
{
    public class GetEventRegistrationsQuery
    : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
        [BindNever]
        public Guid EventId { get; set; }
    }
}