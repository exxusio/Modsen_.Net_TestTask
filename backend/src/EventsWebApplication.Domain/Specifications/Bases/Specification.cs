using System.Linq.Expressions;
using EventsWebApplication.Domain.Specifications.Interfaces;

namespace EventsWebApplication.Domain.Specifications.Bases
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