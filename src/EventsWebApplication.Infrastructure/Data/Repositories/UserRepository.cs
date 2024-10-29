using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class UserRepository(
        AppDbContext context)
        : BaseRepository<User>(context), IUserRepository
    {
    }
}