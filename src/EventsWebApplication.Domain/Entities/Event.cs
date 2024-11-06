using EventsWebApplication.Domain.Entities.Interfaces;
using EventsWebApplication.Domain.Entities.Bases;

namespace EventsWebApplication.Domain.Entities
{
    public class Event
    : BaseModel, IHaveName
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string? ImageUrl { get; set; }
        public int MaxParticipants { get; set; }
        public Guid CategoryId { get; set; }

        public virtual EventCategory Category { get; set; }
        public virtual IEnumerable<EventRegistration> EventRegistrations { get; set; }
    }
}