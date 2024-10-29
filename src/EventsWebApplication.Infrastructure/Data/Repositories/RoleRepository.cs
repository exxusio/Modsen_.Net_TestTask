using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Specifications;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RoleRepository(
        AppDbContext context)
        : BaseRepository<Role>(context), IRoleRepository
    {
        public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default)
        {
            var specification = new RoleByNameSpecification(name);

            var role = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return role;
        }
    }
}