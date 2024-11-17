using MediatR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.UseCases.Users.EventRegistrationCases.Queries.GetRegistrationDetails
{
    public class GetRegistrationDetailsQuery
    : IRequest<EventRegistrationReadDto>
    {
        [BindNever]
        [JsonIgnore]
        public Guid UserId { get; set; }
        [BindNever]
        public Guid EventId { get; set; }
    }
}