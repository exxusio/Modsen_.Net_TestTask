using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Extensions;
using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class RegistrationByEventIdAndParticipantIdSpecification(
        Guid eventId,
        Guid userId
    ) : Specification<EventRegistration>
    {
        public override Expression<Func<EventRegistration, bool>> ToExpression()
        {
            var predicate = PredicateBuilder.True<EventRegistration>();
            predicate = predicate.And(registration => registration.EventId == eventId);
            predicate = predicate.And(registration => registration.ParticipantId == userId);

            return predicate;
        }
    }
}