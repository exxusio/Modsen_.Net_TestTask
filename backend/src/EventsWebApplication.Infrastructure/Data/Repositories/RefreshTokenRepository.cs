using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository(
        AppDbContext context
    ) : BaseRepository<RefreshToken>(context), IRefreshTokenRepository
    {
    }
}