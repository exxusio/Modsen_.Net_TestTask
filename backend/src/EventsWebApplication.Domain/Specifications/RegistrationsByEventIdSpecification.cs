using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class RegistrationsByEventIdSpecification(
        Guid eventId
    ) : Specification<EventRegistration>
    {
        public override Expression<Func<EventRegistration, bool>> ToExpression()
        {
            return registration => registration.EventId == eventId;
        }
    }
}