using EventsWebApplication.Application.DTOs.Events;

namespace EventsWebApplication.Application.DTOs.EventCategories
{
    public class EventCategoryDetailedReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EventReadDto> Events { get; set; }
    }
}