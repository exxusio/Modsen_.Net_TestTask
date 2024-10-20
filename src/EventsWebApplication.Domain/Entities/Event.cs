namespace EventsWebApplication.Domain.Entities
{
    public class Event : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int MaxParticipants { get; set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
    }
}