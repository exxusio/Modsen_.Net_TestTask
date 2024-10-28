using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class RegistrationByEventIdAndParticipantIdSpecification(
        Guid userId,
        Guid eventId)
        : Specification<EventRegistration>
    {
        public override Expression<Func<EventRegistration, bool>> ToExpression()
        {
            var predicate = PredicateBuilder.True<EventRegistration>();
            predicate = predicate.And(registration => registration.ParticipantId == userId);
            predicate = predicate.And(registration => registration.EventId == eventId);

            return predicate;
        }
    }
}