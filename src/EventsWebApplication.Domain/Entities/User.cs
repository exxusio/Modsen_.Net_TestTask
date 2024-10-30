namespace EventsWebApplication.Domain.Entities
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}