namespace EventsWebApplication.Application.DTOs
{
    public class UserDetailedReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}