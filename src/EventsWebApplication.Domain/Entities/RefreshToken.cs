namespace EventsWebApplication.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Key { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }

        public virtual User User { get; set; }
        public bool IsActive => DateTime.UtcNow < ExpirationTime;
    }
}