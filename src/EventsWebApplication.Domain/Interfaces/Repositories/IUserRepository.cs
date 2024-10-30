using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}