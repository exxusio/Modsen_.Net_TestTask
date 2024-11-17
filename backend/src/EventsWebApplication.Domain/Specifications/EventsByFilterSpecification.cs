using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Extensions;
using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Domain.Specifications
{
    public class EventsByFilterSpecification(
        EventFilter filter
    ) : Specification<Event>
    {
        public override Expression<Func<Event, bool>> ToExpression()
        {
            var predicate = PredicateBuilder.True<Event>();

            if (!string.IsNullOrEmpty(filter.EventName))
            {
                predicate = predicate.And(_event => _event.Name.ToLower().Contains(filter.EventName.ToLower()));
            }

            if (filter.FromDate.HasValue)
            {
                predicate = predicate.And(_event => _event.Date >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                predicate = predicate.And(_event => _event.Date <= filter.ToDate.Value);
            }

            if (filter.FromTime.HasValue)
            {
                predicate = predicate.And(_event => _event.Time >= filter.FromTime.Value);
            }

            if (filter.ToTime.HasValue)
            {
                predicate = predicate.And(_event => _event.Time <= filter.ToTime.Value);
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                predicate = predicate.And(_event => _event.Location.ToLower().Contains(filter.Location.ToLower()));
            }

            if (filter.CategoryId.HasValue)
            {
                predicate = predicate.And(_event => _event.CategoryId == filter.CategoryId.Value);
            }

            return predicate;
        }
    }
}