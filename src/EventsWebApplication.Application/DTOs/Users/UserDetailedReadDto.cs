using EventsWebApplication.Application.DTOs.EventRegistrations;
using EventsWebApplication.Application.DTOs.Roles;

namespace EventsWebApplication.Application.DTOs.Users
{
    public class UserDetailedReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public RoleReadDto Role { get; set; }
        public IEnumerable<EventRegistrationReadDto> EventRegistrations { get; set; }
    }
}