using EventsWebApplication.Application.DTOs.EventCategories;

namespace EventsWebApplication.Application.DTOs.Events
{
    public class EventDetailedReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public int MaxParticipants { get; set; }
        public EventCategoryReadDto Category { get; set; }
    }
}