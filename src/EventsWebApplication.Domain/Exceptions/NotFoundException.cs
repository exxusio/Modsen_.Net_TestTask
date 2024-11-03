using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class NotFoundException(
        string message,
        string resource,
        string field,
        string value
    ) : BaseException(message, 404)
    {
        public string Resource { get; } = resource;
        public string Field { get; } = field;
        public string Value { get; } = value;

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {Resource}, Field: {Field}, Value: {Value}";
        }
    }
}