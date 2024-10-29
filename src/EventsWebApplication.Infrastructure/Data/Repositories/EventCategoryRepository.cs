using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Specifications;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class EventCategoryRepository(
        AppDbContext context)
        : BaseRepository<EventCategory>(context), IEventCategoryRepository
    {
        public async Task<EventCategory?> GetCategoryByName(string name, CancellationToken cancellationToken = default)
        {
            var specification = new EventCategoryByNameSpecification(name);

            var category = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return category;
        }
    }
}