using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class RegistrationsByParticipantIdSpecification(
        Guid userId
    ) : Specification<EventRegistration>
    {
        public override Expression<Func<EventRegistration, bool>> ToExpression()
        {
            return registration => registration.ParticipantId == userId;
        }
    }
}