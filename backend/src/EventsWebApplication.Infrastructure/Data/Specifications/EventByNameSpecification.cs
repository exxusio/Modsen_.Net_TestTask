using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class EventByNameSpecification(
        string name
    ) : ByNameSpecification<Event>(name)
    {
    }
}