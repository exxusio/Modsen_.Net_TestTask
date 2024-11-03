using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class NoPermissionException(
        string message,
        string userId,
        string action
    ) : BaseException(message, 403)
    {
        public string? UserId { get; } = userId;
        public string? Action { get; } = action;

        public override string ToString()
        {
            return $"{base.ToString()}, UserID: {UserId}, Action: {Action}";
        }
    }
}