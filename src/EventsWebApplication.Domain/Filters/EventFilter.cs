namespace EventsWebApplication.Domain.Filters
{
    public class EventFilter
    {
        public string? SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public Guid? CategoryId { get; set; }
    }
}