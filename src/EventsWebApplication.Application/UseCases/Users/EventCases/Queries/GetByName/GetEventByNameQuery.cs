using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByName
{
    public class GetEventByNameQuery : IRequest<EventDetailedReadDto>
    {
        public string Name { get; set; }
    }
}