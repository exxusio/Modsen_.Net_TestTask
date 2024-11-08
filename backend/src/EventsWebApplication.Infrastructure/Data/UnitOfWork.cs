using EventsWebApplication.Infrastructure.Data.Repositories;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Domain.Repositories.Bases;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data
{
    public class UnitOfWork(
        AppDbContext dbContext
    ) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly Dictionary<Type, Func<AppDbContext, object>> _repositoryFactories =
        new Dictionary<Type, Func<AppDbContext, object>>
        {
            { typeof(EventRegistration), ctx => new EventRegistrationRepository(ctx) },
            { typeof(EventCategory), ctx => new EventCategoryRepository(ctx) },
            { typeof(RefreshToken), ctx => new RefreshTokenRepository(ctx) },
            { typeof(Event), ctx => new EventRepository(ctx) },
            { typeof(User), ctx => new UserRepository(ctx) },
            { typeof(Role), ctx => new RoleRepository(ctx) }
        };

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repositoryFactories.TryGetValue(typeof(TEntity), out var factory))
            {
                var repository = (IRepository<TEntity>)factory.Invoke(_dbContext);
                return repository;
            }
            else
                throw new NotFoundException(
                    "No repository factory found",
                    nameof(TEntity),
                    "Repository",
                    "RepositoryName"
                );
        }

        public TRepository GetRepository<TRepository, TEntity>()
            where TRepository : IRepository<TEntity>
            where TEntity : class
        {
            if (_repositoryFactories.TryGetValue(typeof(TEntity), out var factory))
            {
                return (TRepository)factory.Invoke(_dbContext);
            }
            else
            {
                throw new NotFoundException(
                    "No repository factory found",
                    nameof(TEntity),
                    "Repository",
                    "RepositoryName"
                );
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}