namespace EventsWebApplication.Domain.Exceptions
{
    public class DuplicateRegistrationException : BaseException
    {
        public string UserId { get; }
        public string EventId { get; }

        public DuplicateRegistrationException(string message, string userId, string eventId)
            : base(message, 409)
        {
            UserId = userId;
            EventId = eventId;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, User ID: {UserId}, Event ID: {EventId}";
        }
    }
}
