namespace EventsWebApplication.Domain.Exceptions
{
    public class DuplicateRegistrationException(
        string message,
        string userId,
        string eventId)
        : BaseException(message, 409)
    {
        public string UserId { get; } = userId;
        public string EventId { get; } = eventId;

        public override string ToString()
        {
            return $"{base.ToString()}, User ID: {UserId}, Event ID: {EventId}";
        }
    }
}