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

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = new
                {
                    UserId,
                    Action
                }
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, UserID: {UserId}, Action: {Action}";
        }
    }
}