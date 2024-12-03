using EventsWebApplication.Domain.Abstractions.Data.Repositories.Bases;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Abstractions.Data.Repositories
{
    public interface IRefreshTokenRepository
    : IRepository<RefreshToken>
    {
    }
}