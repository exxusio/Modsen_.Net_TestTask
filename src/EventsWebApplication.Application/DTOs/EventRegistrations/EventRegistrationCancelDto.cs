namespace EventsWebApplication.Application.DTOs.EventRegistrations
{
    public class EventRegistrationCancelDto
    {
        public Guid EventId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}