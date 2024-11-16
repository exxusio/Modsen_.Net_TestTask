using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class EventExpiredException(
        string message,
        string userId,
        string eventId,
        string dateRegistration
    ) : BaseException(message, 422)
    {
        public string UserId { get; } = userId;
        public string EventId { get; } = eventId;
        public string DateRegistration { get; } = dateRegistration;

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = new
                {
                    UserId,
                    EventId,
                    DateRegistration
                }
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, UserID: {UserId}, EventID: {EventId}, Date: {DateRegistration}";
        }
    }
}