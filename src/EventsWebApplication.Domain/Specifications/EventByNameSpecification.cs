using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class EventByNameSpecification(
        string name
    ) : ByNameSpecification<Event>(name)
    {
    }
}