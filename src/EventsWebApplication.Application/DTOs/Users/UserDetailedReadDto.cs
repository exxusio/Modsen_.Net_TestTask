using EventsWebApplication.Application.DTOs.EventRegistrations;

namespace EventsWebApplication.Application.DTOs.Users
{
    public class UserDetailedReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<EventRegistrationReadDto> EventRegistrations { get; set; }
    }
}