using MediatR;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter
{
    public class GetEventsByFilterQuery
        : PagedQuery, IRequest<IEnumerable<EventReadDto>>
    {
        public string? SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Location { get; set; }
        public Guid? CategoryId { get; set; }
    }
}