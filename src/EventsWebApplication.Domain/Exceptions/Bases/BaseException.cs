namespace EventsWebApplication.Domain.Exceptions.Bases
{
    public abstract class BaseException(
        string message,
        int status
    ) : Exception(message)
    {
        public int Status { get; } = status;

        public virtual object GetErrorDetails()
        {
            return new
            {
                Message,
                Status
            };
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Status: {Status}";
        }
    }
}