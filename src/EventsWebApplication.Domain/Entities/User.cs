using EventsWebApplication.Domain.Entities.Bases;

namespace EventsWebApplication.Domain.Entities
{
    public class User
    : BaseModel
    {
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual IEnumerable<EventRegistration> EventRegistrations { get; set; }
        public virtual IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}