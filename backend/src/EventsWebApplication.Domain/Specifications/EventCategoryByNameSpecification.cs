using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class EventCategoryByNameSpecification(
        string name
    ) : ByNameSpecification<EventCategory>(name)
    {
    }
}