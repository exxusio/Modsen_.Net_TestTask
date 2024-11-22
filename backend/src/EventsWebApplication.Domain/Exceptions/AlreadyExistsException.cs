using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class AlreadyExistsException(
        string message,
        string resource,
        string parameter,
        string value
    ) : BaseException(message)
    {
        public string Resource { get; } = resource;
        public string Parameter { get; } = parameter;
        public string Value { get; } = value;

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = new
                {
                    Resource,
                    Parameter,
                    Value
                }
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {Resource}, Parameter: {Parameter}, Value: {Value}";
        }
    }
}