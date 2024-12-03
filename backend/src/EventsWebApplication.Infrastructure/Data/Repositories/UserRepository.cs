using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Infrastructure.Data.Repositories.Bases;
using EventsWebApplication.Infrastructure.Data.Specifications;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class UserRepository(
        AppDbContext context
    ) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var specification = new UserByEmailSpecification(email);

            var user = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return user;
        }

        public async Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            var specification = new UserByLoginSpecification(login);

            var user = (await _dbSet.Where(specification.ToExpression()).ToListAsync(cancellationToken)).FirstOrDefault();

            return user;
        }
    }
}