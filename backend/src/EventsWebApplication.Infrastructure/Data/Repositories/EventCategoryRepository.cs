using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Infrastructure.Data.Specifications;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventCategoryRepository(
        AppDbContext context
    ) : BaseRepository<EventCategory>(context), IEventCategoryRepository
    {
        public async Task<EventCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var specification = new EventCategoryByNameSpecification(name);

            var category = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return category;
        }
    }
}