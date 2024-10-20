using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.DTOs.EventRegistrations
{
    public class EventRegistrationDetailedReadDto
    {
        public EventReadDto Event { get; set; }
        public UserReadDto Participant { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}