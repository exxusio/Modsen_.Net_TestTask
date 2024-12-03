using EventsWebApplication.Application.Exceptions.Bases;

namespace EventsWebApplication.Application.Exceptions
{
    public class ExpireException : BaseException
    {
        public ExpireException(
            string message
        ) : base(message)
        {
        }
    }
}