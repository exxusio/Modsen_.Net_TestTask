using EventsWebApplication.Domain.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Repositories
{
    public interface IRefreshTokenRepository
    : IRepository<RefreshToken>
    {
    }
}