using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class BadRequestException(
        string message,
        object details
    ) : BaseException(message)
    {
        public object? Details { get; } = details ?? null;

        public override object GetErrorDetails()
        {
            return new
            {
                Info = base.GetErrorDetails(),
                Details = details
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Details: {Details}";
        }
    }
}