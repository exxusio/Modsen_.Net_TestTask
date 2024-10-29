using EventsWebApplication.Domain.Entities.Interfaces;

namespace EventsWebApplication.Domain.Entities
{
    public class EventCategory : BaseModel, IHaveName
    {
        public string Name { get; set; }
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}