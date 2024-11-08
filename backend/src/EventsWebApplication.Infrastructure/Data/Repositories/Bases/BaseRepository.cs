using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Domain.Repositories.Bases;

namespace EventsWebApplication.Infrastructure.Data.Repositories.Bases
{
    public abstract class BaseRepository<TEntity>(
        AppDbContext context
    ) : IRepository<TEntity>
    where TEntity : class
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(item, cancellationToken);
        }

        public void Delete(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}