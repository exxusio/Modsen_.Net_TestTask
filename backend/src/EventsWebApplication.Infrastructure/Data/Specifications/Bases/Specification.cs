using System.Linq.Expressions;
using EventsWebApplication.Infrastructure.Data.Specifications.Interfaces;

namespace EventsWebApplication.Infrastructure.Data.Specifications.Bases
{
    public abstract class Specification<TEntity>
    : ISpecification<TEntity>
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();
    }
}