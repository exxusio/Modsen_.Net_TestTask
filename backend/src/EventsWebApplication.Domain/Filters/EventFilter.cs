namespace EventsWebApplication.Domain.Filters
{
    public class EventFilter
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