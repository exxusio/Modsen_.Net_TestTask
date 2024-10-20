namespace EventsWebApplication.Domain.Entities
{
    public class EventCategory : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}