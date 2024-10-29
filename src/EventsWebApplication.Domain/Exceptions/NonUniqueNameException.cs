namespace EventsWebApplication.Domain.Exceptions
{
    public class NonUniqueNameException(
        string message,
        string resourceName,
        string name)
        : BaseException(message, 409)
    {
        public string ResourceName { get; } = resourceName;
        public string Name { get; } = name;

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}, Name: {Name}";
        }
    }
}