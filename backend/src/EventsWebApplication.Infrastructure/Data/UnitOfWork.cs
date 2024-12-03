using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Abstractions.Data;

namespace EventsWebApplication.Infrastructure.Data
{
    public class UnitOfWork(
        AppDbContext dbContext,
        IEventRegistrationRepository eventRegistrationRepository,
        IEventCategoryRepository eventCategoryRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IEventRepository eventRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository
    ) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IEventRegistrationRepository EventRegistrations { get; } = eventRegistrationRepository;
        public IEventCategoryRepository EventCategories { get; } = eventCategoryRepository;
        public IRefreshTokenRepository RefreshTokens { get; } = refreshTokenRepository;
        public IEventRepository Events { get; } = eventRepository;
        public IUserRepository Users { get; } = userRepository;
        public IRoleRepository Roles { get; } = roleRepository;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}