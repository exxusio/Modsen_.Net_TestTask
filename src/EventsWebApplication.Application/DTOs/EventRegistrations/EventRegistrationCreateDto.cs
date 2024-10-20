namespace EventsWebApplication.Application.DTOs.EventRegistrations
{
    public class EventRegistrationCreateDto
    {
        public Guid EventId { get; set; }
        public Guid ParticipantId { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}