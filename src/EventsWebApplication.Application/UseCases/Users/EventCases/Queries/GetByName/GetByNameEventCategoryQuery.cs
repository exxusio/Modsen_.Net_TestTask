using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByName
{
    public class GetByNameEventQuery : IRequest<EventDetailedReadDto>
    {
        public string Name { get; set; }
    }
}