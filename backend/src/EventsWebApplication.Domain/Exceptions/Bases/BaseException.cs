namespace EventsWebApplication.Domain.Exceptions.Bases
{
    public abstract class BaseException(
        string message
    ) : Exception(message)
    {
        public virtual object GetErrorDetails()
        {
            return new
            {
                Message
            };
        }
    }
}