using MediatR;
using EventsWebApplication.Application.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetUserRegistrations
{
    public class GetUserRegistrationsQuery
    : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
        [BindNever]
        public Guid UserId { get; set; }
    }
}