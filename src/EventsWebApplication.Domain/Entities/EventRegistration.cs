namespace EventsWebApplication.Domain.Entities
{
    public class EventRegistration
    {
        public Guid EventId { get; set; }
        public Guid ParticipantId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual User Participant { get; set; }
    }
}