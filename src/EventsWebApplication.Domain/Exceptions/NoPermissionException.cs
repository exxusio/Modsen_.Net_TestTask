namespace EventsWebApplication.Domain.Exceptions
{
    public class NoPermissionException : BaseException
    {
        public string? UserId { get; }
        public string? Action { get; }

        public NoPermissionException(string message, string action, string? userId = "Undefined")
            : base(message, 403)
        {
            UserId = userId;
            Action = action;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, User ID: {UserId}, Action: {Action}";
        }
    }
}