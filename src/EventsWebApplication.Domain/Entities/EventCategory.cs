using EventsWebApplication.Domain.Entities.Interfaces;
using EventsWebApplication.Domain.Entities.Bases;

namespace EventsWebApplication.Domain.Entities
{
    public class EventCategory
    : BaseModel, IHaveName
    {
        public string Name { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }
    }
}