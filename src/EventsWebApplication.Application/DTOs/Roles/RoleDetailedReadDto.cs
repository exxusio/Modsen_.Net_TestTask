using EventsWebApplication.Application.DTOs.Users;

namespace EventsWebApplication.Application.DTOs.Roles
{
    public class RoleDetailedReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserReadDto> Users { get; set; }
    }
}