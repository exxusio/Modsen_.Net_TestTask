using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
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