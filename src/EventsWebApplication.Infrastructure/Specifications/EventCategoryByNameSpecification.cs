using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Infrastructure.Specifications.Bases;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class EventCategoryByNameSpecification(
        string name)
        : ByNameSpecification<EventCategory>(name)
    {
    }
}