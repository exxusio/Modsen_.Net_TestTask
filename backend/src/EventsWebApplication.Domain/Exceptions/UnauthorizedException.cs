using EventsWebApplication.Domain.Exceptions.Bases;

namespace EventsWebApplication.Domain.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(
            string message
        ) : base(message, 401)
        {
        }
    }
}