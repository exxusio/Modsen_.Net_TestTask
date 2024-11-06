using MediatR;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.UseCases.Bases.Queries.Paged;

namespace EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter
{
    public class GetEventsByFilterQuery
    : PagedQuery, IRequest<IEnumerable<EventReadDto>>
    {
        public string? EventName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public string? Location { get; set; }
        public Guid? CategoryId { get; set; }
    }
}