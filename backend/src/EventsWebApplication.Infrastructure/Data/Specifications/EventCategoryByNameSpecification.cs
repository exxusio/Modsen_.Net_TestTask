using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class EventCategoryByNameSpecification(
        string name
    ) : ByNameSpecification<EventCategory>(name)
    {
    }
}