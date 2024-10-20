using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Entities;

namespace DataAccessLayer.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly Dictionary<Type, Func<AppDbContext, object>> _repositoryFactories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositoryFactories = new Dictionary<Type, Func<AppDbContext, object>>
            {
                { typeof(Event), ctx => new EventRepository(ctx) },
                { typeof(EventRegistration), ctx => new EventRegistrationRepository(ctx) },
                { typeof(User), ctx => new UserRepository(ctx) },
                { typeof(Role), ctx => new RoleRepository(ctx) }
            };
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositoryFactories.TryGetValue(typeof(TEntity), out var factory))
            {
                var repository = (IRepository<TEntity>)factory.Invoke(_dbContext);
                return repository;
            }
            else
                throw new InvalidOperationException($"No repository factory found for type {typeof(TEntity).Name}.");
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}