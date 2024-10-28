using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Infrastructure.Specifications.Bases;

namespace EventsWebApplication.Infrastructure.Specifications
{
    public class RoleByNameSpecification(
        string name)
        : ByNameSpecification<Role>(name)
    {
    }
}