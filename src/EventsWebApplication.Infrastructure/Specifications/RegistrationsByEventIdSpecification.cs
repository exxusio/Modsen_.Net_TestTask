using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class RegistrationsByEventIdSpecification(
        Guid eventId)
        : Specification<EventRegistration>
    {
        public override Expression<Func<EventRegistration, bool>> ToExpression()
        {
            return registration => registration.EventId == eventId;
        }
    }
}