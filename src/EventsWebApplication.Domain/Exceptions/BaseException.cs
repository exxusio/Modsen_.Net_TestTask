namespace EventsWebApplication.Domain.Exceptions
{
    public abstract class BaseException(
        string message,
        int statusCode)
        : Exception(message)
    {
        public int StatusCode { get; } = statusCode;

        public override string ToString()
        {
            return $"{base.ToString()}, Status Code: {StatusCode}";
        }
    }
}