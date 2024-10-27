namespace EventsWebApplication.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public string ResourceName { get; }

        public NotFoundException(string message, string resourceName = "Undefined") : base(message, 404)
        {
            ResourceName = resourceName;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}";
        }
    }
}
