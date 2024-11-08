using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Admins.EventRegistrationCases.Queries.GetRegistrationDetails
{
    public class GetRegistrationDetailsQuery
    : IRequest<EventRegistrationReadDto>
    {
        [BindNever]
        public Guid UserId { get; set; }
        [BindNever]
        public Guid EventId { get; set; }
    }
}