namespace EventsWebApplication.Domain.Exceptions
{
    public class NotFoundException(
        string message,
        string resourceName = "Undefined")
        : BaseException(message, 404)
    {
        public string ResourceName { get; } = resourceName;

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}";
        }
    }
}
