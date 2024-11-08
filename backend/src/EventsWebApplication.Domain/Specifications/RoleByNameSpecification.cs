using EventsWebApplication.Domain.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Specifications
{
    public class RoleByNameSpecification(
        string name
    ) : ByNameSpecification<Role>(name)
    {
    }
}