using EventsWebApplication.Domain.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Repositories
{
    public interface IUserRepository
    : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}