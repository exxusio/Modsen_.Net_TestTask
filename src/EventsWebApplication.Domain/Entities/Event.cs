namespace EventsWebApplication.Domain.Entities
{
    public class Event : BaseModel
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public string Location { get; private set; }
        public string Category { get; private set; }
        public string ImageUrl { get; private set; }
        public int MaxParticipants { get; private set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
    }
}