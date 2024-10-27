using MediatR;
using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetByCriteria
{
    public class GetEventsByCriteriaQuery : IRequest<IEnumerable<EventReadDto>>
    {
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public Guid? CategoryId { get; set; }
    }
}