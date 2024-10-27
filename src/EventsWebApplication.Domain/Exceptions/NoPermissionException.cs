namespace EventsWebApplication.Domain.Exceptions
{
    public class NoPermissionException(
        string message,
        string action,
        string userId = "Undefined")
        : BaseException(message, 403)
    {
        public string? UserId { get; } = userId;
        public string? Action { get; } = action;

        public override string ToString()
        {
            return $"{base.ToString()}, User ID: {UserId}, Action: {Action}";
        }
    }
}