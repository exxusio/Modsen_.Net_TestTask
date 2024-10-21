using EventsWebApplication.Application.DTOs.EventCategories;

namespace EventsWebApplication.Application.DTOs.Events
{
    public class EventCriteriaDto
    {
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public EventCategoryReadDto? Category { get; set; }
    }
}