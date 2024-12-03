using EventsWebApplication.Domain.Abstractions.Data.Repositories;

namespace EventsWebApplication.Domain.Abstractions.Data
{
    public interface IUnitOfWork
    {
        IEventRegistrationRepository EventRegistrations { get; }
        IEventCategoryRepository EventCategories { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IEventRepository Events { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}