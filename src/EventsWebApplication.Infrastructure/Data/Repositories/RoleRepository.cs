using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RoleRepository(
        AppDbContext context)
        : BaseRepository<Role>(context), IRoleRepository
    {
    }
}