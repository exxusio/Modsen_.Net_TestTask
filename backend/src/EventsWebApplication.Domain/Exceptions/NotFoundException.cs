using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class NotFoundException(
        string message,
        string resource,
        string field,
        string value
    ) : BaseException(message)
    {
        public string Resource { get; } = resource;
        public string Field { get; } = field;
        public string Value { get; } = value;

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = new
                {
                    Resource,
                    Field,
                    Value
                }
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Resource: {Resource}, Field: {Field}, Value: {Value}";
        }
    }
}