using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Infrastructure.Data.Specifications;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RoleRepository(
        AppDbContext context
    ) : BaseRepository<Role>(context), IRoleRepository
    {
        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var specification = new RoleByNameSpecification(name);

            var role = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return role;
        }
    }
}