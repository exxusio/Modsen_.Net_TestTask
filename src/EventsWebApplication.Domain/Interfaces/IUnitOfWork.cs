namespace EventsWebApplication.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        TRepository GetRepository<TRepository, TEntity>()
            where TRepository : IRepository<TEntity>
            where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}