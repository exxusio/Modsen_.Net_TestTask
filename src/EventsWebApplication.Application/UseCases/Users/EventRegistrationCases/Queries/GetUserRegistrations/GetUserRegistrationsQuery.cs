using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetUserRegistrations
{
    public class GetUserRegistrationsQuery : IRequest<IEnumerable<EventRegistrationReadDto>>
    {
        [BindNever]
        public Guid UserId { get; set; }
    }
}