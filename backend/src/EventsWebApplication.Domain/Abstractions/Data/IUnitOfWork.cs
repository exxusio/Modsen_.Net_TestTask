using EventsWebApplication.Domain.Abstractions.Data.Repositories.Bases;

namespace EventsWebApplication.Domain.Abstractions.Data
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
        TRepository GetRepository<TRepository, TEntity>()
            where TRepository : IRepository<TEntity>
            where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}