using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById
{
    public class GetEventByIdQuery
    : IRequest<EventReadDto>
    {
        [BindNever]
        public Guid EventId { get; set; }
    }
}