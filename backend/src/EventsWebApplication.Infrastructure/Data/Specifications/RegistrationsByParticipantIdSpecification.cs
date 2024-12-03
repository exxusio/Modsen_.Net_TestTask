using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
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