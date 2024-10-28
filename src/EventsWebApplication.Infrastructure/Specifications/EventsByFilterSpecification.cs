using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class EventsByFilterSpecification(
        EventFilter filter)
        : Specification<Event>
    {
        public override Expression<Func<Event, bool>> ToExpression()
        {
            var predicate = PredicateBuilder.True<Event>();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                predicate = predicate.And(_event => _event.Name.Contains(filter.SearchTerm));
            }

            if (filter.StartDate.HasValue)
            {
                predicate = predicate.And(_event => _event.Date >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                predicate = predicate.And(_event => _event.Date <= filter.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                predicate = predicate.And(_event => _event.Location.Contains(filter.Location));
            }

            if (filter.CategoryId.HasValue)
            {
                predicate = predicate.And(_event => _event.CategoryId == filter.CategoryId.Value);
            }

            return predicate;
        }
    }
}