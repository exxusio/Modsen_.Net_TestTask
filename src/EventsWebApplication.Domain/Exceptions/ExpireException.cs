namespace EventsWebApplication.Domain.Exceptions
{
    public class ExpireException : BaseException
    {
        public ExpireException(
            string message)
            : base(message, 401)
        {
        }
    }
}