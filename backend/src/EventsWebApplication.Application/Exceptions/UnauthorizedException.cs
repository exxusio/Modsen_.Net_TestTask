using EventsWebApplication.Application.Exceptions.Bases;

namespace EventsWebApplication.Application.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(
            string message
        ) : base(message)
        {
        }
    }
}