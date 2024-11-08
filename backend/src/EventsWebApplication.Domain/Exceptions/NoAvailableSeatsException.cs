using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class NoAvailableSeatsException(
        string message,
        string userId,
        string eventId
    ) : BaseException(message, 409)
    {
        public string UserId { get; } = userId;
        public string EventId { get; } = eventId;

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = new
                {
                    UserId,
                    EventId
                }
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, UserID: {UserId}, EventID: {EventId}";
        }
    }
}