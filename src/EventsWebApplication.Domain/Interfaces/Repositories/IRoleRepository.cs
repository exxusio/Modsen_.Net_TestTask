using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}