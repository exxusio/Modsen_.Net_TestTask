namespace EventsWebApplication.Domain.Exceptions
{
    public class NonUniqueNameException : BaseException
    {
        public string ResourceName { get; }
        public string Name { get; }

        public NonUniqueNameException(string message, string resourceName, string name) : base(message, 409)
        {
            ResourceName = resourceName;
            Name = name;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}, Name: {Name}";
        }
    }
}