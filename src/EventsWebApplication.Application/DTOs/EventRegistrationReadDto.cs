using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.DTOs
{
    public class EventRegistrationReadDto
    {
        public Guid Id { get; set; }
        public EventReadDto Event { get; set; }
        public UserReadDto Participant { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}