namespace EventsWebApplication.Application.DTOs
{
    public class EventReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public int MaxParticipants { get; set; }
        public int RegisteredCount { get; set; }
        public bool HasAvailableSeats { get; set; }
        public EventCategoryReadDto Category { get; set; }
    }
}