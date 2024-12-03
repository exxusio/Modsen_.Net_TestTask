using EventsWebApplication.Infrastructure.Data.Specifications.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Specifications
{
    public class RoleByNameSpecification(
        string name
    ) : ByNameSpecification<Role>(name)
    {
    }
}