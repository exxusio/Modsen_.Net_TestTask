using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository(
        AppDbContext context)
        : BaseRepository<RefreshToken>(context), IRefreshTokenRepository
    {
    }
}