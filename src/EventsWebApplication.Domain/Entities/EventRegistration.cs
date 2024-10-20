namespace EventsWebApplication.Domain.Entities
{
    public class EventRegistration
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual User Participant { get; set; }
    }
}