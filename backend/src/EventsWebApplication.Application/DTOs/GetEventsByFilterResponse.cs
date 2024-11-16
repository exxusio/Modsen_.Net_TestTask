namespace EventsWebApplication.Application.DTOs
{
    public class GetEventsByFilterResponse
    {
        public IEnumerable<EventReadDto> Events { get; set; }
        public int TotalCount { get; set; }
    }
}