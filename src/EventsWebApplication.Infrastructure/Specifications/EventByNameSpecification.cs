using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Infrastructure.Specifications.Bases;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class EventByNameSpecification(
        string name)
        : ByNameSpecification<Event>(name)
    {
    }
}