using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository(
        AppDbContext context
    ) : BaseRepository<RefreshToken>(context), IRefreshTokenRepository
    {
    }
}