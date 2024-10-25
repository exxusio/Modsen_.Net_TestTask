namespace EventsWebApplication.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; }
        public string ResourceName { get; }

        public NotFoundException(string message, string resourceName = "Undefined") : base(message)
        {
            ResourceName = resourceName;
            StatusCode = 404;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}, Status Code: {StatusCode}";
        }
    }
}
