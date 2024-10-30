namespace EventsWebApplication.Domain.Exceptions
{
    public class AlreadyExistsException(
        string message,
        string resourceName,
        string parameterName,
        string value)
        : BaseException(message, 409)
    {
        public string ResourceName { get; } = resourceName;
        public string ParameterName { get; } = parameterName;
        public string Value { get; } = value;

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {ResourceName}, Parameter: {ParameterName}, Value: {Value}";
        }
    }
}